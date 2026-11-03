using System;
using System.Collections.Generic;

namespace LFS.Shared.Entities;

public partial class RptFrecuenciaMovimientosPrenda
{
    public int Id { get; set; }

    public string Nombre { get; set; }

    public string Apellido1 { get; set; }

    public string Apellido2 { get; set; }

    public string Vestuario { get; set; }

    public string Taquilla { get; set; }

    public string Descrip { get; set; }

    public int? ConteoPre { get; set; }
}
