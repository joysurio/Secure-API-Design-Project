using System;
using System.Collections.Generic;

namespace LFS.Shared.Entities;

public partial class MovModSubCliente
{
    public int Id { get; set; }

    public int IdSubCliente { get; set; }

    public int IdModeloPrenda { get; set; }

    public virtual MasterModeloPrenda IdModeloPrendaNavigation { get; set; }

    public virtual MasterSubClientes IdSubClienteNavigation { get; set; }
}
