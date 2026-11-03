using System;
using System.ComponentModel.DataAnnotations;

namespace LFS.Shared.Entities
{
    public class FamilyItemModel
    {
        [Key]
        public int Id { get; set; }
        public int? FamilyItemTypeId { get; set; }
        public FamilyItemType FamilyItemType { get; set; }
        [Required]
        [MaxLength(50)]
        public string Description { get; set; }
        public int? MaxWashes { get; set; }
        public int? SupplierId { get; set; }
        public bool NonReturnableDirtyMachine { get; set; }
        public float? TemperatureWashing { get; set; }
        public float? TemperatureDrying { get; set; }
        public DateTime CreationDate { get; set; }
        public bool Deleted { get; set; }
    }
}
