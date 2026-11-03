using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LFS.Shared.ViewModels
{
  public class MultiEditWarehouseRequest
  {
    public List<int> SelectedIds { get; set; }
    public int? IdMaquina { get; set; }
    public int? IdModelo { get; set; }
    public string Talla { get; set; }
    public int? PosX { get; set; }
    public int? PosY { get; set; }
    public int? CantMax { get; set; }
    public int? StockReal { get; set; }
    public int? IdModelo2 { get; set; }
    public string Talla2 { get; set; }
    public int? StockReal2 { get; set; }
    public int? MaxDown { get; set; }
    public int? QuantityGarmentDispense { get; set; }
    public bool LoadGarment { get; set; }
  }

}
