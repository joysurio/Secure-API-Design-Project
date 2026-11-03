using System;
using System.ComponentModel.DataAnnotations;

namespace LFS.Shared.Entities
{
    public class LinenOperation
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string RFIDCode { get; set; } = string.Empty;

        public int ItemId { get; set; }
        public Item Item { get; set; }

        [Required]
        [MaxLength(50)]
        public string OperationType { get; set; } = string.Empty; // "Delivery", "Abnormal", "RandomCheck", "WashedReceived"

        [MaxLength(500)]
        public string? Remark { get; set; }

        [MaxLength(100)]
        public string? DeviceId { get; set; }

        public int? UserId { get; set; }

        public DateTime OperationDate { get; set; } = DateTime.UtcNow;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public bool Deleted { get; set; } = false;
    }
}
