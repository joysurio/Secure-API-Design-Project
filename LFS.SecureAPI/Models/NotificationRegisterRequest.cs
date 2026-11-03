using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LFS.SecureAPI.Models
{
    public class NotificationRegisterRequest
    {
        [Required]
        [JsonPropertyName("platform")]
        public string Platform { get; set; } = string.Empty;

        [Required]
        [JsonPropertyName("token")]
        public string Token { get; set; } = string.Empty;
    }
}
