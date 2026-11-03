using LFS.SecureAPI.Models;

namespace LFS.SecureAPI.Services
{
    /// <summary>
    /// User authentication service (email + password) per CTO MVP spec.
    /// </summary>
    public interface IUserAuthService
    {
        Task<LoginResponse> LoginAsync(LoginRequest request);
    }
}
