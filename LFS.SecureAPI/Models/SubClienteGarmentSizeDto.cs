using System.Text.Json.Serialization;

namespace LFS.SecureAPI.Models
{
    /// <summary>
    /// One row of MovModeloTallaSub with SubCliente and model labels (Size tab).
    /// </summary>
    public class SubClienteGarmentSizeDto
    {
        /// <summary>Primary key of MovModeloTallaSub (use when updating size).</summary>
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("firstName")]
        public string FirstName { get; set; } = string.Empty;

        [JsonPropertyName("lastName1")]
        public string LastName1 { get; set; } = string.Empty;

        [JsonPropertyName("lastName2")]
        public string LastName2 { get; set; } = string.Empty;

        [JsonPropertyName("modelId")]
        public int ModelId { get; set; }

        [JsonPropertyName("modelDescription")]
        public string ModelDescription { get; set; } = string.Empty;

        [JsonPropertyName("size")]
        public string Size { get; set; } = string.Empty;
    }
}
