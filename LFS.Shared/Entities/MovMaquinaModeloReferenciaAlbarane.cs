using System;
using System.Collections.Generic;

namespace LFS.Shared.Entities;

public partial class MovMaquinaModeloReferenciaAlbarane
{
    public int IdMaquina { get; set; }

    public int IdModelo { get; set; }

    public string Referencia { get; set; }

    public virtual MasterMaquinaDispensacion IdMaquinaNavigation { get; set; }

    public virtual MasterModeloPrenda IdModeloNavigation { get; set; }
}
