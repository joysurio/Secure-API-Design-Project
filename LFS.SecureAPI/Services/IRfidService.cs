using LFS.SecureAPI.Models;

namespace LFS.SecureAPI.Services
{
    public interface IRfidService
    {
        Task<ItemDto?> GetItemByRfidAsync(string rfid);
        Task<RfidScanResponse> ScanRfidTagsAsync(RfidScanRequest request);
    }
}
