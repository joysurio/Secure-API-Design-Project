namespace LFS.SecureAPI.Models
{
    public class LinenDeliveryResponse
    {
        public Guid OperationId { get; set; }
        public DateTime ProcessedAt { get; set; }
        public List<LinenDeliveryResult> Results { get; set; } = new();
    }

    public class LinenDeliveryResult
    {
        public string RFID { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty; // "delivered", "not_found", "error"
        public int? ItemId { get; set; }
        public int? OperationId { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
