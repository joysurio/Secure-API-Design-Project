using System;

namespace LFS.Shared.ViewModels
{
  public class StockOfMachinesViewModel
  {
    public int Id { get; set; }
    public string Warehouse { get; set; }  // Nuevo campo
    public string MachineDescription { get; set; }
    public string ModelDescription { get; set; }
    public string Talla { get; set; }
    public int StockReal { get; set; }
    public int CantMax { get; set; }
    public int Porciento { get; set; }
    public int Reposicion { get; set; }
    public int TipoMaquina { get; set; }
  }

}
