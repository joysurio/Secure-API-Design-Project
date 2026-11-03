using LFS.SecureAPI.Models;

namespace LFS.SecureAPI.Services
{
    public interface IMachineService
    {
        Task<MachineDto> GetMachineAsync(int id);
        Task<MachineInventoryResponse> GetMachineInventoryAsync(int id);
        Task<MachineLoadResponse> RegisterMachineLoadAsync(int id, MachineLoadRequest request);
        Task<ReplenishmentResponse> CreateReplenishmentAsync(int id, ReplenishmentRequest request);
        Task<CreditsResponse> ReplenishCreditsAsync(int id, CreditsRequest request);
        Task<DeliveryResponse> RegisterDeliveryAsync(int id, DeliveryRequest request);
    }
}
