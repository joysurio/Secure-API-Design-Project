using LFS.InfraStructure;
using LFS.Shared.Entities;

namespace LFS.SecureAPI.Services
{
    public class AccessLogService : IAccessLogService
    {
        private readonly DataContext _context;

        public AccessLogService(DataContext context)
        {
            _context = context;
        }

        public async Task LogAccessAsync(string endpoint, string method, string? deviceId, int? userId, bool success, int statusCode, string? ipAddress, string? paramsJson = null)
        {
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

            _context.AccessLogs.Add(accessLog);
            await _context.SaveChangesAsync();
        }
    }
}
