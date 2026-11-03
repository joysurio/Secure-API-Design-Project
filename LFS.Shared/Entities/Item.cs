using System;
using System.ComponentModel.DataAnnotations;

namespace LFS.Shared.Entities
{
    public class Item
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(100)]
        public string RFIDCode { get; set; }
        [MaxLength(100)]
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime LastDate { get; set; }
        public int? Cycles { get; set; }
        public int FamilyItemTypeId { get; set; }
        public FamilyItemType FamilyItemType { get; set; }
        public int FamilyItemModelId { get; set; }
        public FamilyItemModel FamilyItemModel { get; set; }
        public int? ItemSizeId { get; set; }
        public ItemSize ItemSize { get; set; }
        public bool Deleted { get; set; }
        public int? LastMov { get; set; }
        public DateTime? DateLastMov { get; set; }
        public bool Personalised { get; set; }
        public int? LastMovementId { get; set; }
        public Movement Movement { get; set; }
        public int? LocationId { get; set; }
        public Location Location { get; set; }
    }
}
