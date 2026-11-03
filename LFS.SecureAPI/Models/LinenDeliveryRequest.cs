using System.ComponentModel.DataAnnotations;

namespace LFS.SecureAPI.Models
{
    public class LinenDeliveryRequest
    {
        [Required]
        public Guid OperationId { get; set; }

        [Required]
        [MaxLength(100)]
        public string PdaId { get; set; } = string.Empty;

        public int? UserId { get; set; }

        [Required]
        public List<LinenDeliveryItem> Items { get; set; } = new();
    }

    public class LinenDeliveryItem
    {
        [Required]
        [MaxLength(100)]
        public string Rfid { get; set; } = string.Empty;

        [MaxLength(500)]
        public string? Remark { get; set; } // e.g., "Nursing room at 12/F"
    }
}
