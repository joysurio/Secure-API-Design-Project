using System;
using System.Collections.Generic;

namespace LFS.Shared.Entities;

public partial class MovMaquinaDisAlmacenes
{
	public int Id { get; set; }

	public int IdMaquinaDispensacion { get; set; }

	public int IdAlmacen { get; set; }

	//public int? Antena { get; set; }

	public int PosEjeX { get; set; }

	public int PosEjeY { get; set; }

	public int CantMax { get; set; }

	public int StockReal { get; set; }

	public int? IdModelo { get; set; }

	public string? Talla { get; set; }

	public int? CantMin { get; set; }

	public int? CantidadPrendaDispensada { get; set; }

	public string PuertoPlccerradura { get; set; }

	public string PuertoPuertaAbierta { get; set; }

	public string CodigoTag { get; set; }

	public int? MayorBajadoPermitido { get; set; }

	public int? IdPrenda { get; set; }

	public int? IdPrenda2 { get; set; }

	public int? IdModelo2 { get; set; }

	public string? Talla2 { get; set; }

	public bool? CargarPrendasFijas { get; set; }

	public virtual MasterMaquinaDispensacion IdMaquinaDispensacionNavigation { get; set; }

	public virtual ICollection<MovMaquinaDisReposicione> MovMaquinaDisReposiciones { get; set; } = new List<MovMaquinaDisReposicione>();

  public int StockReal2 { get; set; }
}
