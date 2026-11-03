using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LFS.Shared.Models
{
  public class GrabaMovPrendaInputModel
  {
    public int Id { get; set; }
    public int IdPrenda { get; set; }
    public int IdModeloPrenda { get; set; }
    public string Talla { get; set; }
    public int IdPuesto { get; set; }
    public int IdTipoAntena { get; set; }
    public int IdOperario { get; set; }
    public string Observaciones { get; set; } = string.Empty;
    public bool Nueva { get; set; } = false;
    public bool MovimientoSalidaAlmacen { get; set; } = false;
    public int IdNuevoCliente { get; set; } = 0;
    public int IdNuevoSubCliente { get; set; } = 0;
    public bool MovimientoEntradaAlmacen { get; set; } = false;
    public int NumeroLote { get; set; } = 0;
    public int? AlbaranLavander { get; set; } = null;
  }
}
