using System;
using System.Collections.Generic;

namespace LFS.Shared.Entities;

public partial class MasterProveedores
{
    public int Id { get; set; }

    public string Nombre { get; set; }

    public string RazonSocial { get; set; }

    public int? IdContactoPrincipal { get; set; }

    public string Cif { get; set; }

    public short? Iva { get; set; }

    public DateTime? FechaAlta { get; set; }

    public DateTime? FechaBaja { get; set; }

    public virtual ICollection<MasterModeloPrenda> MasterModeloPrenda { get; set; } = new List<MasterModeloPrenda>();

    public virtual ICollection<MovDireccionesProveedore> MovDireccionesProveedores { get; set; } = new List<MovDireccionesProveedore>();

    public virtual ICollection<MovTelefonosProveedore> MovTelefonosProveedores { get; set; } = new List<MovTelefonosProveedore>();
}
