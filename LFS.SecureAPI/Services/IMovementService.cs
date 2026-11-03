using LFS.SecureAPI.Models;

namespace LFS.SecureAPI.Services
{
    public interface IMovementService
    {
        Task<MovementResponse> CreateMovementAsync(CreateMovementRequest request);
        Task<MovementResponse> CreateItemMovementAsync(int itemId, CreateItemMovementRequest request);
    }
}
