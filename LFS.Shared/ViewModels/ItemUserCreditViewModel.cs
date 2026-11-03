using System.Collections.Generic;

namespace LFS.Shared.ViewModels
{
  public class ItemUserCreditViewModel
  {
    public int Id { get; set; }
    public int ItemUserId { get; set; }
    public int DepartmentId { get; set; }
    public string DepartmentDescripcion { get; set; }
    public int FamilyItemModelId { get; set; }
    public string ModeloDescripcion { get; set; }
    public int CreditsbyUser { get; set; }
    public int CreditsbyDepartment { get; set; }
    public int Used { get; set; }
    public bool Selected { get; set; }
    public string? Talla { get; set; }
    public List<string> TallasDisponibles { get; set; }
    public bool NewSelected { get; set; }
  }
}
