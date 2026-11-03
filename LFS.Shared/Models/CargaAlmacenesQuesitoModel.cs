using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LFS.Shared.Models
{
  public class CargaAlmacenesQuesitoModel
  {
    public int Id { get; set; }
    public int IdAlmacen { get; set; }
    public int IdModelo { get; set; }
    public string DescripcionModelo { get; set; }
    public string Talla { get; set; }
    public int IdModelo2 { get; set; }
    public string DescripcionModelo2 { get; set; }
    public string Talla2 { get; set; }
    public int StockReal { get; set; }
    public int IdPrenda { get; set; }
    public int IdPrenda2 { get; set; }
  }
}
