using System;
using System.Collections.Generic;

namespace LFS.Shared.Entities;

public partial class MovHistoricosFechasSubCliente
{
    public int Id { get; set; }

    public int IdsubCliente { get; set; }

    public DateTime FechaAlta { get; set; }

    public DateTime? FechaBaja { get; set; }

    public bool NoActivo { get; set; }

    public DateTime FechaModificacion { get; set; }

    public string TipoBaja { get; set; }

    public virtual MasterSubClientes IdsubClienteNavigation { get; set; }
}
