using System;

namespace LFS.Shared.Entities
{
    public class Warehouse
    {
        public int Id { get; set; }
        public int MachineId { get; set; }
        public Machine Machine { get; set; }
        public int Counter { get; set; }
        public int FamilyItemModelId { get; set; }
        public FamilyItemModel FamilyItemModel { get; set; }
        public int? ItemSizeId { get; set; }
        public ItemSize ItemSize { get; set; }
        public int MaximumQuantity { get; set; }
        public int Stock { get; set; }
        public DateTime CreationDate { get; set; }
        public bool Deleted { get; set; }
        public string LastUser { get; set; }
    }
}
