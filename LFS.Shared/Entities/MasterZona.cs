using System;
using System.Collections.Generic;

namespace LFS.Shared.Entities;

public partial class MasterZona
{
    public int Id { get; set; }

    public int? IdZonaPadre { get; set; }

    public string Descripcion { get; set; }

    public int? Orden { get; set; }

    public bool? System { get; set; }

    public virtual MasterZona IdZonaPadreNavigation { get; set; }

    public virtual ICollection<MasterZona> InverseIdZonaPadreNavigation { get; set; } = new List<MasterZona>();

    public virtual ICollection<MovAntenasPuesto> MovAntenasPuestos { get; set; } = new List<MovAntenasPuesto>();
}
