using System;
using System.Collections.Generic;

namespace LFS.Shared.Entities;

public partial class MasterAvisosPorEmail
{
    public int Tipo { get; set; }

    public string Aviso { get; set; }

    public string DireccionEmail { get; set; }

    public int? LimiteNumerico { get; set; }

    public bool? LimiteEsMinimo { get; set; }

    public bool? Habilitado { get; set; }

    public string NumerosSms { get; set; }
}
