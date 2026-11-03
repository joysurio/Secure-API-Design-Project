using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LFS.SecureAPI.Models
{
    public class RefreshRequest
    {
        [Required]
        [JsonPropertyName("refresh_token")]
        public string RefreshToken { get; set; } = string.Empty;
    }
}
