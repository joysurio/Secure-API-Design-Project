using System;
using System.Collections.Generic;

namespace LFS.Shared.Entities;

public partial class MovHistoricoEstadoExternoTaquilla
{
    public int IdTaquilla { get; set; }

    public string DescripcionEstadoExterno { get; set; }

    public DateTime Fecha { get; set; }
}
