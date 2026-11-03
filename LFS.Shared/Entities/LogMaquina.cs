using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LFS.Shared.Entities;

public partial class LogMaquina
{
  public int IdLog { get; set; }

  public int IdMaquinaLog { get; set; }

  public DateTime FechaLog { get; set; }

  public string TipoLog { get; set; }

  public string TrazaLog { get; set; }

  public string ComentarioLog { get; set; }
}
