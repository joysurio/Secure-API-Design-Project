using System;
using System.Collections.Generic;

namespace LFS.Shared.Entities;

public partial class MasterComandosLoader
{
    public int Id { get; set; }

    public string Descrip { get; set; }

    public virtual ICollection<MovComandosAntena> MovComandosAntenas { get; set; } = new List<MovComandosAntena>();
}
