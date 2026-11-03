using System;
using System.Collections.Generic;

namespace LFS.Shared.Entities;

public partial class MasterTipoTag
{
    public int Id { get; set; }

    public string Descripcion { get; set; }

    public bool? System { get; set; }

    public virtual ICollection<MovCambioAntena> MovCambioAntenas { get; set; } = new List<MovCambioAntena>();
}
