using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LFS.Shared.Models
{
  public class MaterTipoAntenaModel
  {
    public int Id { get; set; }

    public string Descripcion { get; set; }

    public bool? MostrarEnPda { get; set; }

    public bool? Mostrar { get; set; }

    public bool NoBorrar { get; set; }

    public bool EsBaja { get; set; }

    public bool PedirUbicacion { get; set; }

    public int? IdGrupo { get; set; }

    public bool? Facturable { get; set; }
  }
}
