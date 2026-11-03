using LFS.SecureAPI.Models;

namespace LFS.SecureAPI.Services
{
    public interface ISupportTicketService
    {
        Task<string> CreateTicketAsync(string userId, SupportTicketRequest request);
        Task<IReadOnlyList<SupportTicketAdminDto>> GetAdminTicketsAsync(string adminUserId, string? status);
        Task ResolveTicketAsync(string adminUserId, string ticketGuid);
    }

    public sealed class SupportTicketAdminDto
    {
        public string TicketGuid { get; set; } = string.Empty;
        public string Status { get; set; } = "open";
        public string Subject { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime CreatedAtUtc { get; set; }
        public string UserDisplayName { get; set; } = string.Empty;
        public string UserRole { get; set; } = string.Empty;
    }
}
