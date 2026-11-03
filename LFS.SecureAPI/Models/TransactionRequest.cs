using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LFS.SecureAPI.Models
{
    /// <summary>
    /// Transaction request for collect/return per CTO MVP spec.
    /// Atomic: insert movement + update credits + update machine stock.
    /// </summary>
    public class TransactionRequest
    {
        [Required]
        [JsonPropertyName("machine_id")]
        public string MachineId { get; set; } = string.Empty;

        [Required]
        [JsonPropertyName("items")]
        public List<TransactionItemRequest> Items { get; set; } = new();

        [Required]
        [JsonPropertyName("type")]
        [RegularExpression("^(collect|return)$", ErrorMessage = "Type must be 'collect' or 'return'")]
        public string Type { get; set; } = "collect";
    }

    public class TransactionItemRequest
    {
        [Required]
        [JsonPropertyName("model_id")]
        public string ModelId { get; set; } = string.Empty;

        [Required]
        [JsonPropertyName("size")]
        [MaxLength(50)]
        public string Size { get; set; } = string.Empty;

        [Required]
        [JsonPropertyName("qty")]
        [Range(1, 1000)]
        public int Qty { get; set; }
    }
}
