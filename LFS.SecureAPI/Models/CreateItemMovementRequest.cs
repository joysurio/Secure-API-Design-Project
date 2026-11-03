using System.ComponentModel.DataAnnotations;

namespace LFS.SecureAPI.Models
{
    public class CreateItemMovementRequest
    {
        [MaxLength(250)]
        public string? Description { get; set; }

        public int? LocationId { get; set; }
    }
}
