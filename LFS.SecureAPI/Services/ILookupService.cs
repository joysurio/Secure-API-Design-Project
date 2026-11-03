using LFS.SecureAPI.Models;

namespace LFS.SecureAPI.Services
{
    public interface ILookupService
    {
        Task<LookupResponse> LookupAsync(LookupRequest request, string[] allowedScopes);
    }
}
