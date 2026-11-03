using System;
using System.Collections.Generic;

namespace LFS.Shared.Entities;

public partial class MovModCli
{
    public int Id { get; set; }

    public int? IdMod { get; set; }

    public int? IdCli { get; set; }
}
