using System;
using System.ComponentModel.DataAnnotations;

namespace LFS.Shared.Entities
{
    public class Device
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string DeviceId { get; set; } = string.Empty;

        [Required]
        [MaxLength(256)]
        public string SecretHash { get; set; } = string.Empty;

        public int? OwnerClientId { get; set; }

        [MaxLength(1000)]
        public string AllowedScopes { get; set; } = string.Empty;

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? LastSeen { get; set; }
    }
}
