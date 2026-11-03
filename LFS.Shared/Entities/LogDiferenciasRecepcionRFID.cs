using System;
using System.Collections.Generic;

namespace LFS.Shared.Entities;

public partial class LogDiferenciasRecepcionRFID
{
    public int Id { get; set; }

    public int IdMaquina { get; set; }

    public DateTime Fecha { get; set; }

    public int? IdUsuarioSeleccionado { get; set; }

    public int? IdModeloSeleccionado { get; set; }

    public int? IdUsuarioLeido { get; set; }

    public int? IdModeloLeido { get; set; }

    public string? CodigoTagLeido { get; set; }

    public DateTime? FechaDispensacionTag { get; set; }
}
