using System;
using System.Collections.Generic;

namespace LFS.Shared.Entities;

public partial class TTerminosFijo
{
    public int Id { get; set; }

    public int? IdprendaTfj { get; set; }

    public DateTime? FechaInicioTfj { get; set; }

    public DateTime? FechaFinalTfj { get; set; }

    public bool? CadaAlbaranTfj { get; set; }

    public bool? CadaFacturaTfj { get; set; }

    public string ObservacionesTfj { get; set; }

    public int? IdSubClienteTfj { get; set; }

    public int? CantPrendasTfj { get; set; }

    public int? IdClienteTfj { get; set; }
}
