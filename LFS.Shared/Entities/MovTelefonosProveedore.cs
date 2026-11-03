using System;
using System.Collections.Generic;

namespace LFS.Shared.Entities;

public partial class MovTelefonosProveedore
{
    public int Id { get; set; }

    public int? IdProveedor { get; set; }

    public int? IdTelefono { get; set; }

    public virtual MasterProveedores IdProveedorNavigation { get; set; }

    public virtual MovTelefono IdTelefonoNavigation { get; set; }
}
