using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LFS.Shared.ViewModels
{
  public class MasterMaquinaDispensacionViewModel
  {
    public int Id { get; set; }
    public string Descripcion { get; set; }
    public bool EsDispensadora { get; set; }
    public bool EsRecolectora { get; set; }
    public bool HayAlmacenVacio { get; set; }
    public bool HayAlmacenConPocasPrendas { get; set; }
    public bool RecolectoraLlena { get; set; }
  }
}
