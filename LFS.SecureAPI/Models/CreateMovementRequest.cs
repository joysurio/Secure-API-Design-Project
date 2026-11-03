using System.ComponentModel.DataAnnotations;

namespace LFS.SecureAPI.Models
{
    public class CreateMovementRequest
    {
        [Required]
        [MaxLength(250)]
        public string Description { get; set; } = string.Empty;
    }
}
