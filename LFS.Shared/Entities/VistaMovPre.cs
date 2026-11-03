using System;
using System.Collections.Generic;

namespace LFS.Shared.Entities;

public partial class VistaMovPre
{
    public string Obser { get; set; }

    public string TipoAntena { get; set; }

    public string NombreMaquina { get; set; }

    public string DesPren { get; set; }

    public int? Nlavados { get; set; }

    public int? Nciclos { get; set; }

    public DateTime? Fecha { get; set; }

    public DateTime? Hora { get; set; }

    public int? Norden { get; set; }

    public int? Ntotal { get; set; }

    public string ModeloPrenda { get; set; }

    public string Nombre { get; set; }

    public string NombreSubCli { get; set; }

    public int? IdPrenda { get; set; }

    public int? IdSubCli { get; set; }

    public int? IdCli { get; set; }

    public int? IdTipoAntena { get; set; }
}
