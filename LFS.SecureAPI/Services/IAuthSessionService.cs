using LFS.SecureAPI.Models;

namespace LFS.SecureAPI.Services
{
    public interface IAuthSessionService
    {
        Task<LoginResponse> RefreshAsync(string refreshToken);
        Task LogoutAsync(string refreshToken);
    }
}
