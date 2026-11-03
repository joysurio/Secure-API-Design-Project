using System;
using System.Collections.Generic;

namespace LFS.Shared.Entities;

public partial class MovDireccionesCliente
{
    public int Id { get; set; }

    public int? IdCliente { get; set; }

    public int? IdDireccion { get; set; }

    public virtual MasterClientes IdClienteNavigation { get; set; }

    public virtual MovDireccione IdDireccionNavigation { get; set; }
}
