using LFS.SecureAPI.Models;

namespace LFS.SecureAPI.Services
{
    public interface IInventoryService
    {
        Task<InventoryCountResponse> SubmitInventoryCountsAsync(InventoryCountRequest request);
        Task<InventoryReconcileResponse> ReconcileInventoryAsync(int locationId);
    }
}
