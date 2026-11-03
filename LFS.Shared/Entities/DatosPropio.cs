using System;
using System.Collections.Generic;

namespace LFS.Shared.Entities;

public partial class DatosPropio
{
    public int Id { get; set; }

    public string Nombre { get; set; }

    public string RazonSocial { get; set; }

    public string Cif { get; set; }

    public int? Iva { get; set; }

    public int? ContadorLotes { get; set; }

    public virtual ICollection<MovDireccionesPropio> MovDireccionesPropios { get; set; } = new List<MovDireccionesPropio>();

    public virtual ICollection<MovTelefonosPropio> MovTelefonosPropios { get; set; } = new List<MovTelefonosPropio>();
}
