using System;
using System.Collections.Generic;

namespace LFS.Shared.Entities;

public partial class MovHistoricoGestionTaquilla
{
    public int IdSubCliente { get; set; }

    public int IdTaquilla { get; set; }

    public int IdEstado { get; set; }

    public DateTime? Fecha { get; set; }

    public virtual MasterSubClientes IdSubClienteNavigation { get; set; }
}
