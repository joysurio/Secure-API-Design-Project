using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LFS.Shared.Models
{
  public class MasterEdificioModel
  {
    public int Id { get; set; }

    public string Descripcion { get; set; }

    public int? IdCliente { get; set; }
  }
}
