using System;
using System.Collections.Generic;

namespace LFS.Shared.Entities;

public partial class AperturaTaquilla
{
    public int? IdSubCli { get; set; }

    public int? IdAlmacen { get; set; }

    public int? IdModelo { get; set; }

    public string Talla { get; set; }

    public DateTime? Fecha { get; set; }
}
