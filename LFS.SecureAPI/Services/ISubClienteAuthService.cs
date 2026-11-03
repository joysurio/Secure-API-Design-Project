using LFS.SecureAPI.Models;

namespace LFS.SecureAPI.Services
{
    /// <summary>
    /// Authentication service for SubCliente (employee) users.
    /// Validates credentials against the Mastersubclientes table.
    /// </summary>
    public interface ISubClienteAuthService
    {
        Task<LoginResponse> LoginAsync(SubClienteLoginRequest request);
        Task<LoginResponse> QrPairAsync(SubClienteQrPairRequest request);
    }
}
