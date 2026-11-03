namespace LFS.SecureAPI.Models
{
    public class RfidScanResponse
    {
        public Guid OperationId { get; set; }
        public DateTime ProcessedAt { get; set; }
        public List<RfidScanResult> Results { get; set; } = new();
    }

    public class RfidScanResult
    {
        public string RFID { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty; // found, created, not_found
        public ItemDto? Item { get; set; }
        public int? MovementId { get; set; }
    }
}
