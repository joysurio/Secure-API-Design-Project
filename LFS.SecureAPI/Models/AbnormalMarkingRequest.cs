using System.ComponentModel.DataAnnotations;

namespace LFS.SecureAPI.Models
{
    public class AbnormalMarkingRequest
    {
        [Required]
        public Guid OperationId { get; set; }

        [Required]
        [MaxLength(100)]
        public string PdaId { get; set; } = string.Empty;

        public int? UserId { get; set; }

        [Required]
        public List<AbnormalItem> Items { get; set; } = new();
    }

    public class AbnormalItem
    {
        [Required]
        [MaxLength(100)]
        public string Rfid { get; set; } = string.Empty;

        [Required]
        [MaxLength(500)]
        public string Remark { get; set; } = string.Empty; // e.g., "Damage, not to use"
    }
}
