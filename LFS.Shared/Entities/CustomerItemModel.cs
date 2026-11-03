using System;

namespace LFS.Shared.Entities
{
    public class CustomerItemModel
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int FamilyItemModelId { get; set; }
        public DateTime CreationDate { get; set; }
        public Customer Customer { get; set; }
        public FamilyItemModel FamilyItemModel { get; set; }
    }
}
