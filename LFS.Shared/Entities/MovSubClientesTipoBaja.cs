using System;
using System.Collections.Generic;

namespace LFS.Shared.Entities;

public partial class MovSubClientesTipoBaja
{
    public int IdSubCliente { get; set; }

    public int IdTipoBaja { get; set; }

    public virtual MasterSubClientes IdSubClienteNavigation { get; set; }
}
