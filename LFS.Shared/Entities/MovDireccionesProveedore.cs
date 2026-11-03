using System;
using System.Collections.Generic;

namespace LFS.Shared.Entities;

public partial class MovDireccionesProveedore
{
    public int Id { get; set; }

    public int? IdProveedor { get; set; }

    public int? IdDireccion { get; set; }

    public virtual MovDireccione IdDireccionNavigation { get; set; }

    public virtual MasterProveedores IdProveedorNavigation { get; set; }
}
