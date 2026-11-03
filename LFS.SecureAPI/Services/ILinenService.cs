using LFS.SecureAPI.Models;

namespace LFS.SecureAPI.Services
{
    public interface ILinenService
    {
        Task<TagRegistrationResponse> RegisterTagsAsync(TagRegistrationRequest request);
        Task<LinenDeliveryResponse> DeliverLinenAsync(LinenDeliveryRequest request);
        Task<AbnormalMarkingResponse> MarkAbnormalAsync(AbnormalMarkingRequest request);
        Task<WashedItemsImportResponse> ImportWashedItemsAsync(WashedItemsImportRequest request);
        Task<RandomCheckResponse> RandomCheckAsync(RandomCheckRequest request);
    }
}
