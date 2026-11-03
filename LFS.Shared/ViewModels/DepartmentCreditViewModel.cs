namespace LFS.Shared.ViewModels
{
    public class DepartmentCreditViewModel
    {
        public int Id { get; set; }
        public int DepartmentId { get; set; }
        public int FamilyItemModelId { get; set; }
        public string Description { get; set; }
        public int Credits { get; set; }
        public bool Selected { get; set; }
    }
}
