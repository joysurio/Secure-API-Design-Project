using System;
using System.Collections.Generic;

namespace LFS.Shared.Entities;

public partial class MasterModeloAntena
{
    public int Id { get; set; }

    public int Numero { get; set; }

    public string Descripcion { get; set; }

    public virtual ICollection<MovAntenasPuesto> MovAntenasPuestos { get; set; } = new List<MovAntenasPuesto>();
}
