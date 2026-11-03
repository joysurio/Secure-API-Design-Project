using System;
using System.Collections.Generic;

namespace LFS.Shared.Entities;

public partial class MasterViasPublica
{
    public int Id { get; set; }

    public string Abreviatura { get; set; }

    public string Descripcion { get; set; }

    public bool? System { get; set; }

    public virtual ICollection<MovDireccione> MovDirecciones { get; set; } = new List<MovDireccione>();
}
