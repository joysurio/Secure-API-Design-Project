using System.Text.Json.Serialization;

namespace LFS.SecureAPI.Models
{
    /// <summary>
    /// Body for PUT /api/me/sizes — use Garments for SubCliente auth, Preferences for app users.
    /// </summary>
    public class MeSizesPutRequest
    {
        [JsonPropertyName("garments")]
        public List<SubClienteGarmentSizeUpdateDto>? Garments { get; set; }

        [JsonPropertyName("preferences")]
        public List<MeSizePreferenceItemDto>? Preferences { get; set; }
    }

    public class MeSizePreferenceItemDto
    {
        [JsonPropertyName("modelId")]
        public string ModelId { get; set; } = string.Empty;

        [JsonPropertyName("size")]
        public string Size { get; set; } = string.Empty;
    }
}
