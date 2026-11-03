namespace LFS.SecureAPI.Models
{
    public class AbnormalMarkingResponse
    {
        public Guid OperationId { get; set; }
        public DateTime ProcessedAt { get; set; }
        public List<AbnormalResult> Results { get; set; } = new();
    }

    public class AbnormalResult
    {
        public string RFID { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty; // "marked", "not_found", "error"
        public int? ItemId { get; set; }
        public int? OperationId { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
