using LFS.SecureAPI.Models;

namespace LFS.SecureAPI.Services
{
    public interface IAuthService
    {
        Task<DeviceRegistrationResponse> RegisterDeviceAsync(DeviceRegistrationRequest request);
        Task<TokenResponse> GetTokenAsync(TokenRequest request);
        Task<bool> RevokeDeviceAsync(string deviceId);
    }
}
