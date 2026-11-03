namespace LFS.SecureAPI.Models
{
    public class OfflineSyncResponse
    {
        public Guid OperationId { get; set; }
        public DateTime ProcessedAt { get; set; }
        public int ProcessedCount { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
