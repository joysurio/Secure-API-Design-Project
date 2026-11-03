using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LFS.Shared.Entities
{
  public class MovPrendasNumeroCiclosExternos
  {
    [Key]
    [ForeignKey(nameof(MasterPrenda))] 
    public int IdMasterPrendas { get; set; }

    public int NumeroCiclos { get; set; }

    public bool Mostrar { get; set; }
    
    public virtual MasterPrendas MasterPrenda { get; set; }
  }
}
