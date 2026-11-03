using System.ComponentModel.DataAnnotations;

namespace LFS.SecureAPI.Models
{
    public class RfidScanRequest
    {
        [Required]
        public Guid OperationId { get; set; }

        [Required]
        [MaxLength(100)]
        public string PdaId { get; set; } = string.Empty;

        public int? UserId { get; set; }

        public bool RegisterMovement { get; set; } = false;

        public bool CreateIfNotFound { get; set; } = false;

        [Required]
        public List<RfidTagRequest> Tags { get; set; } = new();
    }

    public class RfidTagRequest
    {
        [Required]
        [MaxLength(100)]
        public string Rfid { get; set; } = string.Empty;

        public int? Antenna { get; set; }

        public DateTime? Timestamp { get; set; }
    }
}
