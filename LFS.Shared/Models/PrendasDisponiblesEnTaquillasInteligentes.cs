using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LFS.Shared.Models
{
  public class PrendasDisponiblesEnTaquillasInteligentes
  {
    public int Id { get; set; }
    public string Descripcion { get; set; }
    public string Talla { get; set; }  
    public int StockReal { get; set; }
    public int CantidadUso { get; set; }
  }
}
