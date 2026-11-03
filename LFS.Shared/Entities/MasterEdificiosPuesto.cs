using System;
using System.Collections.Generic;

namespace LFS.Shared.Entities;

public partial class MasterEdificiosPuesto
{
    public int Id { get; set; }

    public int IdEdificio { get; set; }

    public int IdPuesto { get; set; }
}
