using System.ComponentModel.DataAnnotations;

namespace LFS.SecureAPI.Models
{
    public class RevokeDeviceRequest
    {
        [Required]
        public string DeviceId { get; set; } = string.Empty;
    }
}
