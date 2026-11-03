using System;
using System.Collections.Generic;

namespace LFS.Shared.Entities;

public partial class MovSubClientesDeGuardium
{
    public int IdSubCliente { get; set; }

    public bool DeGuardia { get; set; }

    public virtual MasterSubClientes IdSubClienteNavigation { get; set; }
}
