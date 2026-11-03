using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LFS.Shared.Entities;

public partial class PuertaDispensacione
{
    [Key]
    public int IdMaquina { get; set; }
    [Key]
    public int? Npuerta { get; set; }

    public int? PosXdispensacion { get; set; }

    public int? PosYdispensacion { get; set; }

    public int? PosXreposo { get; set; }

    public int? PuertoLectorTarjetas { get; set; }

    public string Iplector { get; set; }
}
