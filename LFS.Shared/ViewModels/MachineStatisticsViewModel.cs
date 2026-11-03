using System;

namespace LFS.Shared.ViewModels
{
  public class MachineStatisticsViewModel
  {
    public int Id { get; set; }
    public int MachineId { get; set; }
    public DateTime Date { get; set; }
    public int IdAlmacen { get; set; }
    public int SubClientId { get; set; }
    public int ModelId { get; set; }
    public string Size { get; set; }
    public int? NumeroIntento { get; set; }
    public int? Speed { get; set; }
    public int? PulsosBajado { get; set; }
    public string? Observation { get; set; }
    public string MachineDescription { get; set; }
    public string ModelDescription { get; set; }
    public string SubClientDescription { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndtDate { get; set; }
    public string Description { get; set; }

    public int Dispensations { get; set; }
    public int[] Trys { get; set; } = new int[8];
    public int Loses { get; set; }
    public int NotDispensed { get; set; }
    public int NotDispensedByStore { get; set; }
    public int NotDispensedByGarment { get; set; }
    public int StoreToCero { get; set; }
    public double LosePercent { get; set; }
    public double NotDispensedPercent { get; set; }
    public int IntentosOtroAlmacen { get; set; }


    public MachineStatisticsViewModel()
    {
      StartDate = DateTime.Now; // Asignar la fecha actual como valor predeterminado
      EndtDate = DateTime.Now;
    }
  }

}
