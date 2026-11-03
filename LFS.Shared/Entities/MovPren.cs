using System;
using System.Collections.Generic;

namespace LFS.Shared.Entities;

public partial class MovPren
{
    public int Id { get; set; }

    public int? IdMov { get; set; }

    public int? IdPrenda { get; set; }

    public DateTime? Fecha { get; set; }

    public DateTime? Hora { get; set; }

    public int? IdPuesto { get; set; }

    public int? IdTipoAntena { get; set; }

    public int? IdOperario { get; set; }

    public string Obser { get; set; }

    public int? NumeroLote { get; set; }

    public int? IdCli { get; set; }

    public int? IdSubCli { get; set; }

    public int? AlbaranLavander { get; set; }

    public int? IdModeloPrenda { get; set; }

    public string Talla { get; set; }

    public int? NumeroAlbaranLavander { get; set; }

    public int? AlmacenDispensadora { get; set; }

    public virtual MasterTipoAntena IdTipoAntenaNavigation { get; set; }
}
