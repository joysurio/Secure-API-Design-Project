using System;
using System.ComponentModel.DataAnnotations;

namespace LFS.Shared.Entities
{
    public class ItemSize
    {
        public int Id { get; set; }
        [Required, MaxLength(5)]
        public string Code { get; set; }
        [Required, MaxLength(50)]
        public string Description { get; set; }
        public int FamilyItemModelId { get; set; }
        public FamilyItemModel FamilyItemModel { get; set; }
        public int SortOrder { get; set; }
        public DateTime CreationDate { get; set; }
        public bool Deleted { get; set; }
    }
}
