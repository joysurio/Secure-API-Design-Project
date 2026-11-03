namespace LFS.SecureAPI.Models
{
    public class RandomCheckResponse
    {
        public Guid OperationId { get; set; }
        public DateTime ProcessedAt { get; set; }
        public List<RandomCheckResult> Results { get; set; } = new();
    }

    public class RandomCheckResult
    {
        public string RFID { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty; // "checked", "not_found", "error"
        public int? ItemId { get; set; }
        public int? OperationId { get; set; }
        public DateTime CheckedDate { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
