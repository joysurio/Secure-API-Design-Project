using System;
using System.Collections.Generic;
using System.Text;

namespace LFS.Shared.Models.Old
{
  public class MovMaquinaDisAlmacenesModel
  {
    public int Id { get; set; }
    public int IdMaquinaDispensacion { get; set; }
    public int IdAlmacen { get; set; }
    public int PosEjeX { get; set; }
    public int PosEjeY { get; set; }
    public int CantMax { get; set; }
    public int StockReal { get; set; }
    public int CantidadPrendaDispensada { get; set; }
    public int? IdModelo { get; set; }
    public MasterModeloPrendaModel Modelo { get; set; }
    public string Talla { get; set; }
    public int? IdPrenda { get; set; }
    public int? IdPrenda2 { get; set; }
    public int? IdModelo2 { get; set; }
    public string Talla2 { get; set; }
    public int MayorBajadoPermitido { get; set; }
    public bool? CargarPrendasFijas { get; set; }
    public int StockReal2 { get; set; }
    public int CantidadMinima { get; set; }
    public MasterModeloPrendaModel? Modelo2 { get; set; }

  }
}
