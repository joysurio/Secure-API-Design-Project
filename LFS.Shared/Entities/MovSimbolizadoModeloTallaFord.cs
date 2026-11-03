using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LFS.Shared.Entities;

public partial class MovSimbolizadoModeloTallaFord
{
  [Key]
  public string Cod_articulo { get; set; }

  public string? Descripcion { get; set; }

  public int? Talla { get; set; }

  public int? IdModelo { get; set; }

}
