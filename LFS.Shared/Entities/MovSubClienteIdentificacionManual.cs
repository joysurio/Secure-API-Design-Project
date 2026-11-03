using System;
using System.Collections.Generic;

namespace LFS.Shared.Entities;

public partial class MovSubClienteIdentificacionManual
{
    public int IdSubCliente { get; set; }

    public DateTime Fecha { get; set; }

    public int Id { get; set; }

    public virtual MasterSubClientes IdSubClienteNavigation { get; set; }
}
