using LFS.Shared.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LFS.Shared.Entities;

public partial class MasterMensajesMaquina
{
  public int IdMensajeMaquina { get; set; }

  public int IdMaquina { get; set; }

  public string Mensaje { get; set; }

  public int IdMensaje { get; set; }
    
}
