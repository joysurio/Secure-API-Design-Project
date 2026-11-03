using System;
using System.Collections.Generic;

namespace LFS.Shared.Entities;

public partial class MasterProvincia
{
    public int Id { get; set; }

    public string Codigo { get; set; }

    public string Provincia { get; set; }

    public bool? Mostrar { get; set; }

    public virtual ICollection<MovDireccione> MovDirecciones { get; set; } = new List<MovDireccione>();
}
