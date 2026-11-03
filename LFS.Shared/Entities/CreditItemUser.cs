using System.ComponentModel.DataAnnotations;

namespace LFS.Shared.Entities
{
    public class CreditItemUser
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int FamilyItemModelId { get; set; }
        [Required]
        public FamilyItemModel FamilyItemModel  { get; set; }
        [Required ]
        public int ItemUserId { get; set; }
        [Required]
        public ItemUser ItemUser { get; set; }
        public int ByDepartment { get; set; }
        public int ByUser { get; set; }
        public int Used { get; set; }
    }  
}
