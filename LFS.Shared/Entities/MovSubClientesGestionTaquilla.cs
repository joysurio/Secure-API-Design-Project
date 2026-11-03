using System;
using System.Collections.Generic;

namespace LFS.Shared.Entities;

public partial class MovSubClientesGestionTaquilla
{
    public int IdSubCliente { get; set; }

    public DateTime? FechaPrevistaBaja { get; set; }

    public DateTime? FechaPrevistaAlta { get; set; }

    public virtual MasterSubClientes IdSubClienteNavigation { get; set; }
}
