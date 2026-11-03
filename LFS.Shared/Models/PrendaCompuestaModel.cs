using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LFS.Shared.Models
{
  public class PrendaCompuestaModel
  {
    public int Id { get; set; }
    public string Componente1 { get; set; }
    public string Componente2 { get; set; }
    public byte[] Imagen { get; set; }
  }
}
