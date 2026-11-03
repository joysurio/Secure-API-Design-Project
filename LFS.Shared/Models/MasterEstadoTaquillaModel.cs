using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LFS.Shared.Models
{
  public class MasterEstadoTaquillaModel
  {
    public int Id { get; set; }

    public string Descripcion { get; set; }

    public bool SoloLectura { get; set; }
  }
}
