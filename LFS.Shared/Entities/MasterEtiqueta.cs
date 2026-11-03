using System;
using System.Collections.Generic;

namespace LFS.Shared.Entities;

public partial class MasterEtiqueta
{
    public int Id { get; set; }

    public string Descripcion { get; set; }

    public string ArchivoRpt { get; set; }

    public virtual ICollection<MasterModelosDotacion> MasterModelosDotacions { get; set; } = new List<MasterModelosDotacion>();

    public virtual ICollection<MovEtiquetasCliente> MovEtiquetasClientes { get; set; } = new List<MovEtiquetasCliente>();
}
