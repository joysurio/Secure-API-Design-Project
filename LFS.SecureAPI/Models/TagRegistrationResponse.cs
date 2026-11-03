namespace LFS.SecureAPI.Models
{
    public class TagRegistrationResponse
    {
        public Guid OperationId { get; set; }
        public DateTime ProcessedAt { get; set; }
        public List<TagRegistrationResult> Results { get; set; } = new();
    }

    public class TagRegistrationResult
    {
        public string RFID { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty; // "registered", "already_exists", "error"
        public int? ItemId { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
