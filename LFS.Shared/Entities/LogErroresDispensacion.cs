using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LFS.Shared.Entities;

public partial class LogErroresDispensacion
{
    [Key]
    public int Id { get; set; }

    public int IdMaquina { get; set; }

    public DateTime Fecha { get; set; }

    public int IdAlmacen { get; set; }

    public int? IdUsuario { get; set; }

    public int? IdModelo { get; set; }

    public string Talla { get; set; }

    public int? NumeroIntento { get; set; }

    public int? Velocidad { get; set; }

    public int? PulsosBajados { get; set; }

    public string Observaciones { get; set; }
}
