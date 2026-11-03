using System;
using System.Collections.Generic;

namespace LFS.Shared.Entities;

public partial class MasterMaquinaDispensacion
{
  public int Id { get; set; }

  public string Descripcion { get; set; }

  public int? IdCliente { get; set; }

  public int? IdTipoMaquina { get; set; }

  public int? PuertoPlc { get; set; }

  public int? PuertoLectorTarjetas { get; set; }

  public int? PuertoPlc2 { get; set; }

  public int? PuertoVisorMaquinaSucio { get; set; }

  public string IdentificadorLogo { get; set; }

  public int? IdPuesto { get; set; }

  public bool? PedirCodigoNuevoUsuario { get; set; }

  public int? CantHuecos { get; set; } = 1;

  public string CtoServTecnico { get; set; }

  public bool CodigoManual { get; set; }

  public string Iplector { get; set; }

  public int? TipoAntenaUhf { get; set; }

  public DateTime? HoraReposicion { get; set; }

  public string Observaciones { get; set; }

  public string EmailAviso { get; set; }

  public int? IdAlmacenCentral { get; set; }

  public string ResolucionPantalla { get; set; }

  public int? IdModeloDftDer { get; set; }

  public int? IdModeloDftIzq { get; set; }

  public int? MovimientoPredeterminado { get; set; }

  /// <summary>
  /// Machine-specific dirty-garment capacity for collectors (type 4).
  /// When null or 0, fall back to <c>MasterAvisosPorEmail.LimiteNumerico</c> where Tipo = 4.
  /// </summary>
  public int? NumeroPrendasSucias { get; set; }

  public virtual MasterPuesto IdPuestoNavigation { get; set; }

  public virtual MasterTipoMaquinaDispensadora IdTipoMaquinaNavigation { get; set; }

  public virtual ICollection<MaquinasDispensadorasParametro> MaquinasDispensadorasParametros { get; set; } = new List<MaquinasDispensadorasParametro>();

  public virtual ICollection<MovCargasDescargasMaquina> MovCargasDescargasMaquinas { get; set; } = new List<MovCargasDescargasMaquina>();

  public virtual ICollection<MovMaquinaDisAlmacenes> MovMaquinaDisAlmacenes { get; set; } = new List<MovMaquinaDisAlmacenes>();

  // Relación Muchos a Muchos
  public virtual ICollection<MovMaquinaDepartamentos> MovMaquinaDepartamentos { get; set; }
}
