using System;
using System.Collections.Generic;

namespace LFS.Shared.Entities;

public partial class MasterNivelAcceso
{
    public int Id { get; set; }

    public string Descripcion { get; set; }

    public int? Nivel { get; set; }

    public virtual ICollection<MasterTiposUsuario> MasterTiposUsuarios { get; set; } = new List<MasterTiposUsuario>();
}
