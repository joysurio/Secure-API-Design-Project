using System;
using System.Collections.Generic;

namespace LFS.Shared.Entities;

public partial class MovMaquinaDisReposicione
{
    public int Id { get; set; }

    public int IdMaqDisAlm { get; set; }

    public int? IdModelo { get; set; }

    public string Talla { get; set; }

    public int CantRepuesta { get; set; }

    public DateTime Fecha { get; set; }

    public int? IdUsuario { get; set; }

    public int? IdAlmacen { get; set; }

    public int? CantEnMaquina { get; set; }

    public virtual MovMaquinaDisAlmacenes IdMaqDisAlmNavigation { get; set; }

    public virtual MasterModeloPrenda IdModeloNavigation { get; set; }
}
