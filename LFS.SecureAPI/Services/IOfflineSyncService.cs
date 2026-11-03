using LFS.SecureAPI.Models;

namespace LFS.SecureAPI.Services
{
    public interface IOfflineSyncService
    {
        Task<OfflineSyncResponse> SyncOfflineOperationsAsync(OfflineSyncRequest request);
    }
}
