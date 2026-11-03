using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LFS.Shared.Models
{
  public class MasterVestuarioModel
  {
    public int Id { get; set; }

    public string Descripcion { get; set; }

    public int? TipoVestuario { get; set; }

    public int? IdCliente { get; set; }

    public int? IdEdificio { get; set; }
  }
}
