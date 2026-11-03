using System;
using System.Collections.Generic;

namespace LFS.Shared.Entities;

public partial class MovSopl
{
    public string Id { get; set; }

    public int? IdPuesto { get; set; }

    public int? Col { get; set; }

    public int? Row { get; set; }

    public int? IdCliente { get; set; }

    public int? IdSubCliente { get; set; }

    public DateTime? DateLastMov { get; set; }

    public int? Estado { get; set; }

    public int? ColPlc { get; set; }

    public int? RowPlc { get; set; }

    public int TipoPosicionSopl { get; set; }

    public int? LedInicio { get; set; }

    public int? LedFinal { get; set; }
}
