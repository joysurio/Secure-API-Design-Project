namespace LFS.SecureAPI.Models
{
    public class InventoryCountResponse
    {
        public Guid OperationId { get; set; }
        public DateTime ProcessedAt { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
