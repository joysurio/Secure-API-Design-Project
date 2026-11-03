using System;
using System.Collections.Generic;

namespace LFS.Shared.Entities;

public partial class MovEtiquetasCliente
{
    public int Id { get; set; }

    public int? IdCliente { get; set; }

    public int? IdEtiqueta { get; set; }

    public virtual MasterClientes IdClienteNavigation { get; set; }

    public virtual MasterEtiqueta IdEtiquetaNavigation { get; set; }
}
