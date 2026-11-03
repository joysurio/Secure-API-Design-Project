using System;
using System.Collections.Generic;

namespace LFS.Shared.Entities;

public partial class MasterPrendas
{
  public int Id { get; set; }

  /// <summary>
  /// SELECT [id], [descripcion] FROM MasterModeloPrenda;
  /// </summary>
  public int? IdModeloPrenda { get; set; }

  /// <summary>
  /// SELECT [id], [nombre] FROM MasterClientes;
  /// </summary>
  public int? IdCliente { get; set; }

  /// <summary>
  /// SELECT [id], [nombre] &amp; [APELLIDO1] FROM MasterSubClientes;
  /// </summary>
  public int? IdSubCliente { get; set; }

  public string CodigoTag { get; set; }

  public string Descrip { get; set; }

  public DateTime? Falta { get; set; }

  public DateTime? Fbaja { get; set; }

  public bool? Nueva { get; set; }

  public int? Nlavados { get; set; }

  public string Taquilla { get; set; }

  public string Color { get; set; }

  public bool? Accesorio { get; set; }

  public string Talla { get; set; }

  public int? IdEtiqueta { get; set; }

  public bool? Borrado { get; set; }

  public int? Nciclos { get; set; }

  public int? Norden { get; set; }

  public int? Ntotal { get; set; }

  public int? LastMov { get; set; }

  public DateTime? DateLastMov { get; set; }

  public DateTime? FbajaReal { get; set; }

  public int? Stock { get; set; }

  public bool? Facturable { get; set; }

  public int? IdMaqLastMov { get; set; }

  public bool Personalizada { get; set; }

  public int? LastIdMov { get; set; }

  public int? ComprobacionLecturas { get; set; }

  public int? IdUbicacion { get; set; }

  public int? IdTipoRopa { get; set; }

  public double? TemperaturaLavadoRopa { get; set; }

  public double? TemperaturaSecadoRopa { get; set; }

  public int ModeloComprobacion { get; set; }

  public bool? NoActualizarModeloTalla { get; set; }

  public virtual MasterClientes IdClienteNavigation { get; set; }

  public virtual MasterModeloPrenda IdModeloPrendaNavigation { get; set; }

  public virtual MasterSubClientes IdSubClienteNavigation { get; set; }

  public virtual MasterTipoRopa IdTipoRopaNavigation { get; set; }

  public virtual MovPrendasNumeroCiclosExternos MovPrendasNumeroCiclosExternos { get; set; }
}
