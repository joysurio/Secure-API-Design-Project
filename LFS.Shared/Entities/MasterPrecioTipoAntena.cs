using System;
using System.Collections.Generic;

namespace LFS.Shared.Entities;

public partial class MasterPrecioTipoAntena
{
    public int Id { get; set; }

    public int IdCliente { get; set; }

    public int IdModeloPrenda { get; set; }

    public int IdTipoAntena { get; set; }

    public double? Precio { get; set; }

    public virtual MasterClientes IdClienteNavigation { get; set; }

    public virtual MasterModeloPrenda IdModeloPrendaNavigation { get; set; }

    public virtual MasterTipoAntena IdTipoAntenaNavigation { get; set; }
}
