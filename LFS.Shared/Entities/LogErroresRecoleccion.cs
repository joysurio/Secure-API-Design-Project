using System;
using System.Collections.Generic;

namespace LFS.Shared.Entities;

public partial class LogErroresRecoleccion
{
    public int Id { get; set; }

    public int IdMaquina { get; set; }

    public DateTime Fecha { get; set; }

    public int? IdUsuario { get; set; }

    public int? IdModelo { get; set; }

    public bool? HaLeidoUsuarioDiferente { get; set; }

    public bool? HaLeidoModeloDiferente { get; set; }

    public string Observaciones { get; set; }
}
