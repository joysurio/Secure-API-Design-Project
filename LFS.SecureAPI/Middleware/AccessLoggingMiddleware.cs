using LFS.InfraStructure;
using LFS.Shared.Entities;
using System.Text;
using System.Text.Json;

namespace LFS.SecureAPI.Middleware
{
    public class AccessLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<AccessLoggingMiddleware> _logger;
        private readonly IServiceScopeFactory _scopeFactory;

        public AccessLoggingMiddleware(
            RequestDelegate next,
            ILogger<AccessLoggingMiddleware> logger,
            IServiceScopeFactory scopeFactory)
        {
            _next = next;
            _logger = logger;
            _scopeFactory = scopeFactory;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Capture request details
            var endpoint = context.Request.Path.Value ?? string.Empty;
            var method = context.Request.Method;
            var ipAddress = context.Connection.RemoteIpAddress?.ToString();

            // Get user/device info from claims
            var userIdClaim = context.User?.FindFirst("sub")?.Value;
            int? userId = null;
            string? deviceId = null;

            if (int.TryParse(userIdClaim, out var parsedUserId))
            {
                userId = parsedUserId;
            }
            else
            {
                deviceId = userIdClaim; // If not a number, it's a device ID
            }

            // Read request body for logging (if needed)
            string? paramsJson = null;
            if (context.Request.ContentLength > 0 && 
                context.Request.ContentType?.Contains("application/json") == true)
            {
                context.Request.EnableBuffering();
                var bodyStream = new StreamReader(context.Request.Body, Encoding.UTF8, leaveOpen: true);
                var body = await bodyStream.ReadToEndAsync();
                context.Request.Body.Position = 0;

                // Limit size to avoid logging huge payloads
                if (body.Length < 10000)
                {
                    paramsJson = body;
                }
            }

            // Capture response
            var originalBodyStream = context.Response.Body;
            using var responseBody = new MemoryStream();
            context.Response.Body = responseBody;

            bool success = false;
            int statusCode = 500;
            Exception? pipelineException = null;

            try
            {
                await _next(context);
                success = context.Response.StatusCode >= 200 && context.Response.StatusCode < 300;
                statusCode = context.Response.StatusCode;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception processing {Method} {Path}", method, endpoint);
                pipelineException = ex;
                statusCode = 500;
                success = false;
            }
            finally
            {
                // Copy whatever the pipeline wrote (or nothing on exception) back to the real stream.
                responseBody.Seek(0, SeekOrigin.Begin);
                await responseBody.CopyToAsync(originalBodyStream);

                // Log to database asynchronously (fire and forget to not block the response).
                _ = Task.Run(async () =>
                {
                    try
                    {
                        using var scope = _scopeFactory.CreateScope();
                        var scopedContext = scope.ServiceProvider.GetRequiredService<DataContext>();

                        var accessLog = new AccessLog
                        {
                            TimestampUtc = DateTime.UtcNow,
                            UserId = userId,
                            DeviceId = deviceId,
                            Endpoint = endpoint,
                            Method = method,
                            ParamsJson = paramsJson,
                            Success = success,
                            ResponseStatus = statusCode,
                            IpAddress = ipAddress
                        };

                        scopedContext.AccessLogs.Add(accessLog);
                        await scopedContext.SaveChangesAsync();
                    }
                    catch (Exception ex)
                    {
                        // Only log as warning if it's a table missing error, otherwise error
                        if (ex is Microsoft.EntityFrameworkCore.DbUpdateException dbEx && 
                            dbEx.InnerException is Microsoft.Data.SqlClient.SqlException sqlEx && 
                            sqlEx.Number == 208) // Invalid object name
                        {
                            _logger.LogWarning("AccessLog table does not exist. Please run the database migration or CREATE_SECURITY_TABLES.sql script.");
                        }
                        else
                        {
                            _logger.LogError(ex, "Error logging access to database");
                        }
                    }
                });
            }

            // Re-throw so ASP.NET Core's exception handler can write a proper 500 response.
            // This must happen AFTER the finally block copies the (possibly empty) body.
            if (pipelineException is not null)
                System.Runtime.ExceptionServices.ExceptionDispatchInfo.Capture(pipelineException).Throw();
        }
    }
}
