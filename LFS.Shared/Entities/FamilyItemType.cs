using System;
using System.ComponentModel.DataAnnotations;

namespace LFS.Shared.Entities
{
    public class FamilyItemType
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Description { get; set; }
        public int? MaxWashes { get; set; }
        public DateTime CreationDate { get; set; }
        public bool Deleted { get; set; }
    }
}
