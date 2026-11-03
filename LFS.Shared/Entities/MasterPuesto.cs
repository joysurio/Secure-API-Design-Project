using System;
using System.Collections.Generic;

namespace LFS.Shared.Entities;

public partial class MasterPuesto
{
    public int Id { get; set; }

    public string NombreMaquina { get; set; }

    public bool? Activo { get; set; }

    public string HotKey { get; set; }

    public int? IdZona { get; set; }

    public int? ColumnasLeds { get; set; }

    public int? FilasLeds { get; set; }

    public bool? Clasificacion { get; set; }

    public virtual ICollection<MasterMaquinaDispensacion> MasterMaquinaDispensacions { get; set; } = new List<MasterMaquinaDispensacion>();

    public virtual ICollection<MovAntenasPuesto> MovAntenasPuestos { get; set; } = new List<MovAntenasPuesto>();
}
