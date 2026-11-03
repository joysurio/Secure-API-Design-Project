using System;
using System.Collections.Generic;

namespace LFS.Shared.Entities;

public partial class VerificacionFuncionamiento
{
    public int Id { get; set; }

    public DateTime Fecha { get; set; }

    public int IdMaquina { get; set; }
}
