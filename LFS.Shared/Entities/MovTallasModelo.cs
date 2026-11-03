using System;
using System.Collections.Generic;

namespace LFS.Shared.Entities;

public partial class MovTallasModelo
{
    public int Id { get; set; }

    public int IdModeloPrenda { get; set; }

    public string Talla { get; set; }

    public int Ordenenlalista { get; set; }

    public virtual MasterModeloPrenda IdModeloPrendaNavigation { get; set; }
}
