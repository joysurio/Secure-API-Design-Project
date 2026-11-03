using System;
using System.Collections.Generic;

namespace LFS.Shared.Entities;

public partial class MovAntenasPuesto
{
    public int Id { get; set; }

    public int? IdPuesto { get; set; }

    public int? IdTipoAntena { get; set; }

    public int? IdUltimoTipoAntena { get; set; }

    public int? IdZona { get; set; }

    public string Com { get; set; }

    public bool? ReadSound { get; set; }

    public bool? PrintLabel { get; set; }

    public bool? Matricial { get; set; }

    public string NombreImpresoraDirecta { get; set; }

    public bool? PrintPren { get; set; }

    public int? Modelo { get; set; }

    public int? IdEmpresa { get; set; }

    public virtual MasterPuesto IdPuestoNavigation { get; set; }

    public virtual MasterTipoAntena IdTipoAntenaNavigation { get; set; }

    public virtual MasterZona IdZonaNavigation { get; set; }

    public virtual MasterModeloAntena ModeloNavigation { get; set; }
}
