using System;
using System.Collections.Generic;

namespace LFS.Shared.Entities;

public partial class MasterDotacione
{
    public int Id { get; set; }

    public int IdCliente { get; set; }

    public string Descripcion { get; set; }

    public virtual ICollection<MasterModelosDotacion> MasterModelosDotacions { get; set; } = new List<MasterModelosDotacion>();
}
