using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LFS.Shared.Entities;

public partial class MasterTaquilla
{
  [Key]
  public int Id { get; set; }

  public string Descripcion { get; set; }

  public int? IdVestuario { get; set; }

  public int? Estado { get; set; }

  public int? EstadoLlave1 { get; set; }

  public int? EstadoLlave2 { get; set; }

  public string Observaciones { get; set; }

  public int? Usuario { get; set; }

  public string Campo1 { get; set; }

  public string Campo2 { get; set; }

  public int? Centro { get; set; }

  public DateTime? DateLastMov { get; set; }

  public DateTime? FechaEntrega { get; set; }

  public int? ModeloTaquilla { get; set; }

  public int? IdEstadoExterno { get; set; }
}
