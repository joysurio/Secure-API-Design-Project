using System;
using System.Collections.Generic;

namespace LFS.Shared.Entities;

public partial class MovCargasDescargasMaquina
{
    public int Id { get; set; }

    public int TipoMov { get; set; }

    public int IdMaquina { get; set; }

    public int IdUsuario { get; set; }

    public DateTime FechaHora { get; set; }

    public virtual MasterMaquinaDispensacion IdMaquinaNavigation { get; set; }

    public virtual MasterUsuario IdUsuarioNavigation { get; set; }
}
