using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LFS.SecureAPI.Models
{
    /// <summary>
    /// Update payload for a single MovModeloTallaSub row.
    /// Prefer <see cref="Id"/> when known; clients may send only <see cref="ModelId"/> (maps to Idmod for this SubCliente).
    /// </summary>
    public class SubClienteGarmentSizeUpdateDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("modelId")]
        public int? ModelId { get; set; }

        [Required]
        [MaxLength(50)]
        [JsonPropertyName("size")]
        public string Size { get; set; } = string.Empty;
    }
}
