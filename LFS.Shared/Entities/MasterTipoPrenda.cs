using System;
using System.Collections.Generic;

namespace LFS.Shared.Entities;

public partial class MasterTipoPrenda
{
    public int Id { get; set; }

    public string Descripcion { get; set; }

    public int? MaxLavados { get; set; }

    public virtual ICollection<MasterModeloPrenda> MasterModeloPrenda { get; set; } = new List<MasterModeloPrenda>();
}
