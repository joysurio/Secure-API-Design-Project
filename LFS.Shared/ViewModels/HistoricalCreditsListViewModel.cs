using System;

namespace LFS.Shared.ViewModels
{
  public class HistoricalCreditsListViewModel
  {
    public string MachineDescription { get; set; }
    public int MachineId { get; set; }
    public string ModelDescription { get; set; }
    public int ModelId { get; set; }
    public string ClientDescription { get; set; }
    public int? ClientId { get; set; }
    public string SubClientDescription { get; set; }
    public int SubClientId { get; set; }
    public string User { get; set; }
    public int MoveTypeId { get; set; }
    public string MoveTypeDescription { get; set; }
    public DateTime Date { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndtDate { get; set; }
    public int StockInUse { get; set; }
    public string Description { get; set; }

    public HistoricalCreditsListViewModel()
    {
      StartDate = DateTime.Now; // Asignar la fecha actual como valor predeterminado
      EndtDate = DateTime.Now;
    }
  }

}
