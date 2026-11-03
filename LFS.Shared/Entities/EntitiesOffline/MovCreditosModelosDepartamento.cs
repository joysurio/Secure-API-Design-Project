using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LFS.Shared.Entities.EntitiesOffline
{
  public partial class MovCreditosModelosDepartamento
  {
    public int IdDepartamento { get; set; }

    public int IdModelo { get; set; }

    public int TipoRango { get; set; }

    public int LimiteCantidadPrendasEnUso { get; set; }
  }
}
