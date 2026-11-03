using System;
using System.Collections.Generic;

namespace LFS.Shared.Entities;

public partial class MasterCategoriaIncidencium
{
    public int Id { get; set; }

    public string Descripcion { get; set; }

    public bool? System { get; set; }

    public virtual ICollection<MasterTipoIncidencium> MasterTipoIncidencia { get; set; } = new List<MasterTipoIncidencium>();
}
