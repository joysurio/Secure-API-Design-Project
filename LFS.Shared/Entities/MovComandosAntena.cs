using System;
using System.Collections.Generic;

namespace LFS.Shared.Entities;

public partial class MovComandosAntena
{
    public int Id { get; set; }

    public int? IdComando { get; set; }

    public string CodigoTag { get; set; }

    public int? IdAntena { get; set; }

    public virtual MasterComandosLoader IdComandoNavigation { get; set; }
}
