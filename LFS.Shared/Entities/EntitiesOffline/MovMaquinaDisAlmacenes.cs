using System.ComponentModel.DataAnnotations;

namespace LFS.Shared.Entities.EntitiesOffline
{
  public class MovMaquinaDisAlmacenes : MovMaquinaDisAlmacenesBase
  {
    public int? IdModelo { get; set; }
  }

  public class MovMaquinaDisAlmacenesBase
  {
    public int Id { get; set; }
    public int IdMaquinaDispensacion { get; set; }

    public int IdAlmacen { get; set; }
    public int PosEjeX { get; set; }
    public int PosEjeY { get; set; }
    public int CantMax { get; set; }
    public int StockReal { get; set; }

    public int? IdModelo { get; set; }

    [MaxLength(50)]
    public string? Talla { get; set; }
    public int? CantMin { get; set; }
    public int? CantidadPrendaDispensada { get; set; }

    public int? IdPrenda { get; set; }

    public int? IdPrenda2 { get; set; }

    public int? IdModelo2 { get; set; }

    [MaxLength(50)]
    public string? Talla2 { get; set; }

    public int? MayorBajadoPermitido { get; set; }

    public string PuertoPlccerradura { get; set; }
  }
}
