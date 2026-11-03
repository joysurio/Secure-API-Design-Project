using System.ComponentModel.DataAnnotations;

namespace LFS.SecureAPI.Models
{
    public class LookupRequest
    {
        [Required]
        [MaxLength(100)]
        public string Entity { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string KeyField { get; set; } = string.Empty;

        [Required]
        [MaxLength(500)]
        public string KeyValue { get; set; } = string.Empty;

        [Required]
        public List<string> Fields { get; set; } = new();

        public List<string>? IncludeRelated { get; set; }
    }
}
