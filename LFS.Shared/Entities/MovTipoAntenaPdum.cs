using System;
using System.Collections.Generic;

namespace LFS.Shared.Entities;

public partial class MovTipoAntenaPdum
{
    public int IdTipoMov { get; set; }

    public int IdLastMov { get; set; }

    public int Id { get; set; }

    public int? Orden { get; set; }
}
