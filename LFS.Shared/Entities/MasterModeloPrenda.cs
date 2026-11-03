using System;
using System.Collections.Generic;

namespace LFS.Shared.Entities;

public partial class MasterModeloPrenda
{
  public int Id { get; set; }

  public int? IdTipoPrenda { get; set; }

  public string Descripcion { get; set; }

  public int? MaxLavados { get; set; }

  public int? IdProveedor { get; set; }

  public int TipoPosicionSopl { get; set; }

  public bool NoRetornableMaquinaSucio { get; set; }

  public int? ModeloComodin { get; set; }

  public int? IdTipoRopa { get; set; }

  public double? TemperaturaLavadoRopa { get; set; }

  public double? TemperaturaSecadoRopa { get; set; }
  public byte[] Imagen { get; set; }
  public decimal Precio { get; set; }

  public virtual MasterProveedores IdProveedorNavigation { get; set; }

  public virtual MasterTipoPrenda IdTipoPrendaNavigation { get; set; }

  public virtual ICollection<MasterModelosDotacion> MasterModelosDotacions { get; set; } = new List<MasterModelosDotacion>();

  public virtual ICollection<MasterPrecioTipoAntena> MasterPrecioTipoAntenas { get; set; } = new List<MasterPrecioTipoAntena>();

  public virtual ICollection<MasterPrendas> MasterPrenda { get; set; } = new List<MasterPrendas>();

  public virtual ICollection<MovMaquinaDisReposicione> MovMaquinaDisReposiciones { get; set; } = new List<MovMaquinaDisReposicione>();

  public virtual ICollection<MovModSubCliente> MovModSubClientes { get; set; } = new List<MovModSubCliente>();

  public virtual ICollection<MovModeloTallaSub> MovModeloTallaSubs { get; set; } = new List<MovModeloTallaSub>();

  public virtual ICollection<MovStockPrendasUsu> MovStockPrendasUsus { get; set; } = new List<MovStockPrendasUsu>();

  public virtual ICollection<MovTallasModelo> MovTallasModelos { get; set; } = new List<MovTallasModelo>();

  public virtual ICollection<MasterModeloPrendaComposicion> ComoComponente { get; set; }

  public virtual ICollection<MasterModeloPrendaComposicion> ComoCompuesta { get; set; }
}
