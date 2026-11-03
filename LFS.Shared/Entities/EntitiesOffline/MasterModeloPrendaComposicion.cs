using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LFS.Shared.Entities
{
  public class MasterModeloPrendaComposicion
  {
    public int IdPrendaCompuesta { get; set; }
    public int IdPrendaComponente { get; set; }
    
    public virtual MasterModeloPrenda PrendaCompuesta { get; set; }
    public virtual MasterModeloPrenda PrendaComponente { get; set; }
  }
}
