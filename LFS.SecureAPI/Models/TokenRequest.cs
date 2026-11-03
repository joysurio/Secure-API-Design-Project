using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LFS.SecureAPI.Models
{
    public class TokenRequest
    {
        [Required]
        [JsonPropertyName("grant_type")]
        public string GrantType { get; set; } = string.Empty;

        [Required]
        [JsonPropertyName("deviceId")]
        public string DeviceId { get; set; } = string.Empty;

        [Required]
        [JsonPropertyName("secret")]
        public string Secret { get; set; } = string.Empty;
    }
}
