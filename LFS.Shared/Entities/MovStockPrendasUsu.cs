using System;
using System.Collections.Generic;

namespace LFS.Shared.Entities;

public partial class MovStockPrendasUsu
{
    public int Id { get; set; }

    public int IdSubCliente { get; set; }

    public int IdModelo { get; set; }

    public string Talla { get; set; }

    public int Cantidad { get; set; }

    public virtual MasterModeloPrenda IdModeloNavigation { get; set; }

    public virtual MasterSubClientes IdSubClienteNavigation { get; set; }
}
