using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LFS.Shared.Entities
{
  public class IntentosLoginFallidos

  {
    [Key]
    public int Id { get; set; } // Autoincremental y llave primaria
    public string UsuarioEmail { get; set; }
    public int IntentosFallidos { get; set; }
    public DateTime FechaUltimoIntento { get; set; }

  }
}
