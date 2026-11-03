using System;
using System.ComponentModel.DataAnnotations;

namespace LFS.Shared.Entities
{
    public class FamilyItem
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Description { get; set; }
        public int MaxUsers { get; set; }
        public bool NotNecessaryReturn { get; set; }
        public DateTime CreationDate { get; set; }
        public bool Deleted { get; set; }
    }
}
