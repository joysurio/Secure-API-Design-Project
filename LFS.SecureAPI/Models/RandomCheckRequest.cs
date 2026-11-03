using System.ComponentModel.DataAnnotations;

namespace LFS.SecureAPI.Models
{
    public class RandomCheckRequest
    {
        [Required]
        public Guid OperationId { get; set; }

        [Required]
        [MaxLength(100)]
        public string PdaId { get; set; } = string.Empty;

        public int? UserId { get; set; }

        [Required]
        public List<string> RfidList { get; set; } = new();
    }
}
