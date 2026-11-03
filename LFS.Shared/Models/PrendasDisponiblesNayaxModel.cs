using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LFS.Shared.Models
{
  public class PrendasDisponiblesNayaxModel
  {
    public int Id { get; set; }
    public string Descripcion { get; set; }
    public int StockReal { get; set; }
    public byte[]? Imagen { get; set; }
    public ushort Codigo { get; set; }
    public decimal Precio { get; set; }
  }
}
