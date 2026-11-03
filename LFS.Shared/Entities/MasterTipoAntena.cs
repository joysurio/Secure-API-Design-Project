using System;
using System.Collections.Generic;

namespace LFS.Shared.Entities;

public partial class MasterTipoAntena
{
    public int Id { get; set; }

    public string Descripcion { get; set; }

    public bool? MostrarEnPda { get; set; }

    public bool Mostrar { get; set; }

    public bool NoBorrar { get; set; }

    public bool EsBaja { get; set; }

    public bool PedirUbicacion { get; set; }

    public int? IdGrupo { get; set; }

    public bool? Facturable { get; set; }

    public virtual ICollection<MasterPrecioTipoAntena> MasterPrecioTipoAntenas { get; set; } = new List<MasterPrecioTipoAntena>();

    public virtual ICollection<MovAntenasPuesto> MovAntenasPuestos { get; set; } = new List<MovAntenasPuesto>();

    public virtual ICollection<MovCambioAntena> MovCambioAntenas { get; set; } = new List<MovCambioAntena>();

    public virtual ICollection<MovPren> MovPrens { get; set; } = new List<MovPren>();
}
