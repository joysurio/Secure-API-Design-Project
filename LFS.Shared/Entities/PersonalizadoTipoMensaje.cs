using LFS.Shared.Entities;
using LFS.Shared.Enums;
using System.Collections.Generic;

namespace LFS.Shared.Entities
{
  public class PersonalizadoTipoMensaje
  {
    public int IdMensaje { get; set; }
    public string TipoMensaje { get; set; }
    public string Mensaje { get; set; } = string.Empty;
    public List<int> IdsMaquinas { get; set; }
    public List<string> NombresMaquinas { get; set; }
  }
}
