using System;
using System.Collections.Generic;

namespace LFS.Shared.Entities;

public partial class MovTelefonosPropio
{
    public int Id { get; set; }

    public int? IdPropio { get; set; }

    public int? IdTelefono { get; set; }

    public virtual DatosPropio IdPropioNavigation { get; set; }

    public virtual MovTelefono IdTelefonoNavigation { get; set; }
}
