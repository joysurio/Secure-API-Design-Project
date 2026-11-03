using System;
using System.Collections.Generic;

namespace LFS.Shared.Entities;

public partial class MovDireccionesPropio
{
    public int Id { get; set; }

    public int? IdPropio { get; set; }

    public int? IdDireccion { get; set; }

    public virtual MovDireccione IdDireccionNavigation { get; set; }

    public virtual DatosPropio IdPropioNavigation { get; set; }
}
