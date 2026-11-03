using LFS.SecureAPI.Models;

namespace LFS.SecureAPI.Services
{
    public interface IMovPrenService
    {
        Task<InsertMovPrenResponse> InsertMovPrenAsync(InsertMovPrenRequest request);
    }
}
