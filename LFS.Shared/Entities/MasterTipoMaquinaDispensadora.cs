using System;
using System.Collections.Generic;

namespace LFS.Shared.Entities;

public partial class MasterTipoMaquinaDispensadora
{
    public int Id { get; set; }

    public string Descripcion { get; set; }

    public virtual ICollection<MasterMaquinaDispensacion> MasterMaquinaDispensacions { get; set; } = new List<MasterMaquinaDispensacion>();
}
