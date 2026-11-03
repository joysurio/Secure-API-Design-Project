using System;
using System.Collections.Generic;

namespace LFS.Shared.Entities;

public partial class MasterEdificio
{
    public int Id { get; set; }

    public string Descripcion { get; set; }

    public int? IdCliente { get; set; }
}
