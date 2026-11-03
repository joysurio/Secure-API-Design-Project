using System.ComponentModel.DataAnnotations;

namespace LFS.Shared.Entities
{
    public class Sex
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Description { get; set; }
        public int SortOrder { get; set; }
    }
}
