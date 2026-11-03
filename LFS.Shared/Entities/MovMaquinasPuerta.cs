using System;
using System.Collections.Generic;

namespace LFS.Shared.Entities;

public partial class MovMaquinasPuerta
{
  public int IdMaquina { get; set; }

  public int? Npuerta { get; set; }

  public int? PosX { get; set; }

  public int? PosY { get; set; }

  public DateTime? FechaHoraPeticion { get; set; }

  public bool? Procesada { get; set; }

  public DateTime? FechaHoraDispensacion { get; set; }

  public int? Nreintentos { get; set; }

  public int? MaximoBajado { get; set; }

  public int Id { get; set; }

  public int? ResultadoDispensacion { get; set; }

  public int? VelocidadRevueltaPinza { get; set; }

  public int MaximoBajadoPermitido { get; set; }

  public int DistanciaBalda { get; set; }

}
