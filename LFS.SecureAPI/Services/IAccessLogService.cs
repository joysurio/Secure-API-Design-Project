namespace LFS.SecureAPI.Services
{
    public interface IAccessLogService
    {
        Task LogAccessAsync(string endpoint, string method, string? deviceId, int? userId, bool success, int statusCode, string? ipAddress, string? paramsJson = null);
    }
}
