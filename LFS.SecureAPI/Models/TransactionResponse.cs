using System.Text.Json.Serialization;

namespace LFS.SecureAPI.Models
{
    /// <summary>
    /// Transaction response per CTO MVP spec.
    /// </summary>
    public class TransactionResponse
    {
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;

        [JsonPropertyName("status")]
        public string Status { get; set; } = "pending";
    }
}
