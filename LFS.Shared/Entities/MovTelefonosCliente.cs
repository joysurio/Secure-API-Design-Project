using System;
using System.Collections.Generic;

namespace LFS.Shared.Entities;

public partial class MovTelefonosCliente
{
    public int Id { get; set; }

    public int? IdCliente { get; set; }

    public int? IdTelefono { get; set; }

    public virtual MasterClientes IdClienteNavigation { get; set; }

    public virtual MovTelefono IdTelefonoNavigation { get; set; }
}
