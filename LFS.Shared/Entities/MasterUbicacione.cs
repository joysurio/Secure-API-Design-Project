using System;
using System.Collections.Generic;

namespace LFS.Shared.Entities;

public partial class MasterUbicacione
{
    public int Id { get; set; }

    public string Pasillo { get; set; }

    public string Posicion { get; set; }

    public string Altura { get; set; }

    public string Descripcion { get; set; }
}
