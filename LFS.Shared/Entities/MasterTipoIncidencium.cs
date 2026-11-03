using System;
using System.Collections.Generic;

namespace LFS.Shared.Entities;

public partial class MasterTipoIncidencium
{
    public int Id { get; set; }

    public int? IdCategoria { get; set; }

    public string Descripcion { get; set; }

    public bool? System { get; set; }

    public virtual MasterCategoriaIncidencium IdCategoriaNavigation { get; set; }
}
