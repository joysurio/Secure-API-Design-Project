using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LFS.Shared.Entities.EntitiesOffline
{
  public class MovStockPrendasUsuarioTotal
  {
    public int Id { get; set; }
    public int IdSubCliente { get; set; }
    public int PrendasEnUso { get; set; }

    public virtual MasterSubClientes SubCliente { get; set; }
  }
}
