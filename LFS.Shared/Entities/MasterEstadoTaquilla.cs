using System;
using System.Collections.Generic;

namespace LFS.Shared.Entities;

public partial class MasterEstadoTaquilla
{
    public int Id { get; set; }

    public string Descripcion { get; set; }

    public bool SoloLectura { get; set; }
}
