using System.ComponentModel.DataAnnotations;

namespace LFS.Shared.Entities
{
    public class CreditModelDepartment
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int DepartmentId { get; set; }
        public Department Department { get; set; }
        [Required]
        public int FamilyItemModelId { get; set; }
        public FamilyItemModel FamilyItemModel { get; set; }
        public int Credits { get; set; }
    }
}
