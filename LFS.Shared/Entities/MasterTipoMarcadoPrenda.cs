using System;
using System.Collections.Generic;

namespace LFS.Shared.Entities;

public partial class MasterTipoMarcadoPrenda
{
    public int IdTpm { get; set; }

    public string Descripcion { get; set; }

    public virtual ICollection<MasterClientes> MasterClientes { get; set; } = new List<MasterClientes>();
}
