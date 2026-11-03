using System;
using System.Collections.Generic;

namespace LFS.Shared.Entities;

public partial class MasterTipoRopa
{
    public int Id { get; set; }

    public string Descripcion { get; set; }

    public virtual ICollection<MasterPrendas> MasterPrenda { get; set; } = new List<MasterPrendas>();
}
