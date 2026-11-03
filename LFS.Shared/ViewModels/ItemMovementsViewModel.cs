using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LFS.Shared.ViewModels
{
  public class ItemMovementsViewModel
  {
    public int Id { get; set; }
    public string Movimiento { get; set; }
    public int IdMovimiento { get; set; }
    public DateTime Fecha { get; set; }
    public string Prenda { get; set; }
    public string Cliente { get; set; }
    public string Usuario { get; set; }
    public string Talla { get; set; }
    public string DNI { get; set; }
    public string CodigoTag { get; set; }
  }
}