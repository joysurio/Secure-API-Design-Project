using System;

namespace LFS.Shared.ViewModels
{
  public class GarmentInventoryListViewModel
  {
    public int IdCliente { get; set; }
    public string Cliente { get; set; }
    public int IdUsuario { get; set; }
    public string NombreUsuario { get; set; }
    public int IdMov { get; set; }
    public int IdTipoMovimiento { get; set; }
    public string TipoMovimiento { get; set; }
    public string Departamento { get; set; }
    public int IdModelo { get; set; }
    public string Modelo { get; set; }
    public string Talla { get; set; }
    public string Tag { get; set; }
    public DateTime FechaCreacion { get; set; }
    public DateTime FechaLastMov { get; set; }
    public int Ciclos { get; set; }
    public int Cantidad { get; set; }
    public int IdMaquina { get; set; }
    public string Maquina { get; set; }
    public int CantidadUsuarios { get; set; }
    public int CantidadModelos { get; set; }
    public string PorcentajeCiclo25 { get; set; }
    public int cantidadCiclo25 { get; set; }
    public string PorcentajeCiclo50 { get; set; }
    public int cantidadCiclo50 { get; set; }
    public string PorcentajeCiclo100 { get; set; }
    public int cantidadCiclo100 { get; set; }
    public string PorcentajeCiclo100Mas { get; set; }
    public int cantidadCiclo100Mas { get; set; }

  }

}
