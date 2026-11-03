using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LFS.Shared.ViewModels
{
  public class WarehouseIndexViewModel
  {
    public int Id { get; set; }
    public int IdAlmacen { get; set; }
    public int IdMaquina { get; set; }
    public int CantMax { get; set; }
    public int? CantMin { get; set; }
    public string Modelo { get; set; }
    public int? IdModelo { get; set; }
    public string Tipo { get; set; }
    public string Talla { get; set; }
    public int PosEjeX { get; set; }
    public int PosEjeY { get; set; }
    public int StockReal { get; set; }
    public int StockReal2 { get; set; }
    public int? IdCliente { get; set; }
    public string Cliente { get; set; }
    public string PuertoPlccerradura { get; set; }
    public string PuertoPuertaAbierta { get; set; }
    public string CodigoTag { get; set; }
    public int? MayorBajadoPermitido { get; set; }
    public int? IdModelo2 { get; set; }
    public string Talla2 { get; set; }
    public int? IdPrenda { get; set; }
    public int? IdPrenda2 { get; set; }
    public bool? CargarPrendasFijas { get; set; }
    public int? CantidadPrendaDispensada { get; set; }
    public IEnumerable<SelectListItem> ModelosCliente { get; set; }
    public string NombreMaquinaResolved { get; set; }
  }
}