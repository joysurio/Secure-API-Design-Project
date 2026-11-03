using System.ComponentModel.DataAnnotations;

namespace LFS.SecureAPI.Models
{
    public class DeviceRegistrationRequest
    {
        [Required]
        [MaxLength(100)]
        public string DeviceId { get; set; } = string.Empty;

        public int? OwnerClientId { get; set; }

        [Required]
        public string AllowedScopes { get; set; } = string.Empty;

        [Required]
        public string SecretPlain { get; set; } = string.Empty;
    }
}
