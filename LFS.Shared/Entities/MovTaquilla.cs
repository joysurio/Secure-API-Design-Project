using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LFS.Shared.Entities;

public partial class MovTaquilla
{  
  [Key]
  public int Id { get; set; }

  public DateTime? Fecha { get; set; }

  public int? TipoMovimiento { get; set; }

  public int? IdTaquilla { get; set; }

  public int? IdUsuario { get; set; }

  public int? Nllave { get; set; }
}
