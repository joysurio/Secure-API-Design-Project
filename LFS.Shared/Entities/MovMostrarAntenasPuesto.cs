using System;
using System.Collections.Generic;

namespace LFS.Shared.Entities;

public partial class MovMostrarAntenasPuesto
{
    public int IdPuesto { get; set; }

    public int IdTipoAntena { get; set; }

    public virtual MasterPuesto IdPuestoNavigation { get; set; }

    public virtual MasterTipoAntena IdTipoAntenaNavigation { get; set; }
}
