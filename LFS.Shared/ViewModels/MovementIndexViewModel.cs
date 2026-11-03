using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LFS.Shared.ViewModels
{
  public class MovementIndexViewModel
  {
    public int Id { get; set; }
    public string Cliente { get; set; }
    public string Usuario { get; set; }
    public string Taquilla { get; set; }
    public string Prenda { get; set; }
    public string Talla { get; set; }
    public int? IdMovimiento { get; set; }
    public string OrdenDeTotal { get; set; }
    public int? IdPrenda { get; set; }
    public string CodigoTag { get; set; }
    public string Movimiento { get; set; }
    public DateTime? Fecha { get; set; }
    public int IdMaquina { get; set; }
    public string NombreMaquina { get; set; }

  }
}