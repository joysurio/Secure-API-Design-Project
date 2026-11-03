using System;
using System.ComponentModel.DataAnnotations;

namespace LFS.Shared.Entities
{
    public class AccessLog
    {
        [Key]
        public int Id { get; set; }

        public DateTime TimestampUtc { get; set; } = DateTime.UtcNow;

        public int? UserId { get; set; }

        [MaxLength(100)]
        public string? DeviceId { get; set; }

        [Required]
        [MaxLength(200)]
        public string Endpoint { get; set; } = string.Empty;

        [Required]
        [MaxLength(10)]
        public string Method { get; set; } = string.Empty;

        public string? ParamsJson { get; set; }

        public bool Success { get; set; }

        public int ResponseStatus { get; set; }

        [MaxLength(45)]
        public string? IpAddress { get; set; }
    }
}
