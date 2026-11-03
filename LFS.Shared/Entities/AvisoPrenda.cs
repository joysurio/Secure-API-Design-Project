using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LFS.Shared.Entities;

public partial class AvisoPrenda
{
    public int? IdPre { get; set; }

    public bool? Borra { get; set; }

    public int? IdAntena { get; set; }

    public DateTime? DateOpen { get; set; }

    public DateTime? DateClose { get; set; }

    public int? TipoAviso { get; set; }

    public string Comments { get; set; }
  
    public int Id { get; set; }
}
