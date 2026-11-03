using System;
using System.Collections.Generic;

namespace LFS.Shared.Entities;

public partial class MovModeloTallaSub
{
    public int Id { get; set; }

    public int? IdSubCli { get; set; }

    public int? Idmod { get; set; }

    public string Talla { get; set; }

    public virtual MasterSubClientes IdSubCliNavigation { get; set; }

    public virtual MasterModeloPrenda IdmodNavigation { get; set; }
}
