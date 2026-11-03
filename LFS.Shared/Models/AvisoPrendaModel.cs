using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LFS.Shared.Models
{
  public class AvisoPrendaModel
  {
    public int IdCliente { get; set; }
    public int IdModelo { get; set; }
    public string Talla { get; set; }
    public int IdPrenda { get; set; }
    public int IdTipoAviso { get; set; }
    public int IdTipoAntena { get; set; }
    public string Observaciones { get; set; }
    public bool SoloUnaVez { get; set; }
    public int IdPuesto { get; set; }
    public bool MostrarTipoAntenaSinMovimiento { get; set; }
  }
}
