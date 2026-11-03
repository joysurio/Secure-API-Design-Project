using System;
using System.Collections.Generic;

namespace LFS.Shared.Entities;

public partial class MovCambioAntena
{
    public string CodigoTag { get; set; }

    /// <summary>
    /// SELECT MasterTipoAntena.descripcion, MasterTipoAntena.Id FROM MasterTipoAntena;
    /// </summary>
    public int? IdTipoantena { get; set; }

    /// <summary>
    /// SELECT [id], [descripcion] FROM MasterTipoTag;
    /// </summary>
    public int? IdTipoTag { get; set; }

    public virtual MasterTipoTag IdTipoTagNavigation { get; set; }

    public virtual MasterTipoAntena IdTipoantenaNavigation { get; set; }
}
