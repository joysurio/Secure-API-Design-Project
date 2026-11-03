namespace LFS.SecureAPI.Models
{
    public class WashedItemsImportResponse
    {
        public Guid OperationId { get; set; }
        public DateTime ProcessedAt { get; set; }
        public DateTime ReceivedDate { get; set; }
        public int TotalItems { get; set; }
        public int FoundItems { get; set; }
        public int NotFoundItems { get; set; }
        public List<WashedItemResult> Results { get; set; } = new();
    }

    public class WashedItemResult
    {
        public string RFID { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty; // "received", "not_found", "error"
        public int? ItemId { get; set; }
        public int? OperationId { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
