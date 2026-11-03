using System.ComponentModel.DataAnnotations;

namespace LFS.SecureAPI.Models
{
    public class OfflineSyncRequest
    {
        [Required]
        public Guid OperationId { get; set; }

        [Required]
        public string DeviceId { get; set; } = string.Empty;

        public int? UserId { get; set; }

        [Required]
        public List<OfflineOperation> Operations { get; set; } = new();
    }

    public class OfflineOperation
    {
        [Required]
        public string Type { get; set; } = string.Empty; // reposicion_add, inventory_count, scan

        public string? Payload { get; set; } // JSON payload
    }
}
