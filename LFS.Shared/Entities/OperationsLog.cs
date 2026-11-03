using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LFS.Shared.Entities
{
    public class OperationsLog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid OperationId { get; set; }

        [MaxLength(100)]
        public string? DeviceId { get; set; }

        public int? UserId { get; set; }

        [Required]
        [MaxLength(200)]
        public string Endpoint { get; set; } = string.Empty;

        [Required]
        [MaxLength(64)]
        public string PayloadHash { get; set; } = string.Empty;

        public DateTime ProcessedAt { get; set; } = DateTime.UtcNow;
    }
}
