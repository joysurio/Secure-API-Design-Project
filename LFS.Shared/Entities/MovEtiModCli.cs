using System;
using System.Collections.Generic;

namespace LFS.Shared.Entities;

public partial class MovEtiModCli
{
    public int Id { get; set; }

    public int? IdMod { get; set; }

    public int? IdCli { get; set; }

    public int? IdEti { get; set; }
}
