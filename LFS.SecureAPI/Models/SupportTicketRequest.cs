using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LFS.SecureAPI.Models
{
    public class SupportTicketRequest
    {
        [JsonPropertyName("machine_id")]
        public int? MachineId { get; set; }

        [Required]
        [MaxLength(200)]
        [JsonPropertyName("subject")]
        public string Subject { get; set; } = string.Empty;

        [Required]
        [MaxLength(2000)]
        [JsonPropertyName("description")]
        public string Description { get; set; } = string.Empty;

        [MaxLength(1000)]
        [JsonPropertyName("image_url")]
        public string? ImageUrl { get; set; }
    }
}
