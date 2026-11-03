using System;
using System.Collections.Generic;

namespace LFS.Shared.Entities;

public partial class MasterVestuario
{
    public int Id { get; set; }

    public string Descripcion { get; set; }

    public int? TipoVestuario { get; set; }

    public int? IdCliente { get; set; }

    public int? IdEdificio { get; set; }
}
