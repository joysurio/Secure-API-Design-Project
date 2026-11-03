using System.ComponentModel.DataAnnotations;

namespace LFS.SecureAPI.Models
{
    public class TagRegistrationRequest
    {
        [Required]
        public Guid OperationId { get; set; }

        [Required]
        [MaxLength(100)]
        public string PdaId { get; set; } = string.Empty;

        public int? UserId { get; set; }

        [Required]
        public List<TagRegistrationItem> Tags { get; set; } = new();
    }

    public class TagRegistrationItem
    {
        [Required]
        [MaxLength(100)]
        public string Rfid { get; set; } = string.Empty;

        [MaxLength(100)]
        public string? Description { get; set; }

        public int? FamilyItemModelId { get; set; }

        public int? ItemSizeId { get; set; }

        public int? LocationId { get; set; }
    }
}
