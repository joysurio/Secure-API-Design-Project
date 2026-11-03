using System;
using System.Collections.Generic;

namespace LFS.Shared.Entities;

public partial class MasterTiposUsuario
{
    public int Id { get; set; }

    public string Descripcion { get; set; }

    public int? IdNivelAcceso { get; set; }

    public virtual MasterNivelAcceso IdNivelAccesoNavigation { get; set; }
}
