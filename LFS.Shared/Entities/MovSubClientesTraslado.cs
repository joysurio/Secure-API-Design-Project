using System;
using System.Collections.Generic;

namespace LFS.Shared.Entities;

public partial class MovSubClientesTraslado
{
    public int IdSubCliente { get; set; }

    public string DescripcionTraslado { get; set; }

    public DateTime FechaTraslado { get; set; }

    public virtual MasterSubClientes IdSubClienteNavigation { get; set; }
}
