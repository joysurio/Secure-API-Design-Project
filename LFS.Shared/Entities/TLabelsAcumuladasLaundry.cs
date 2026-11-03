using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LFS.Shared.Entities;

public partial class TLabelsAcumuladasLaundry
{
  [Key]
  public int Id { get; set; }

  public string Nombre { get; set; }

  public string Apellido1 { get; set; }

  public string Departamento { get; set; }

  public string Empresa { get; set; }

  public string Taquilla { get; set; }

  public string Prenda { get; set; }

  public decimal? IdPre { get; set; }

  public string CodTagPre { get; set; }

  public string Apellido2 { get; set; }

  public string Campo1 { get; set; }

  public string Modelo { get; set; }

  public string Vestuario { get; set; }

  public string Code128 { get; set; }

  public string NombreCliente { get; set; }

  public int? Norden { get; set; }

  public int? Ntotal { get; set; }

  public string Talla { get; set; }

  public int? IdDepartamento { get; set; }
}
