using System;
using System.Collections.Generic;

namespace LFS.Shared.Entities;

public partial class MasterTiposDireccione
{
    public int Id { get; set; }

    public string Denominacion { get; set; }

    /// <summary>
    /// 1=Domicilio Social,2=Correspondencia,3=Envio MercancÃ­a,4=Sucursal;5=Almacen (0=def por usuario)
    /// </summary>
    public int? Tipo { get; set; }

    public bool? System { get; set; }

    public virtual ICollection<MovDireccione> MovDirecciones { get; set; } = new List<MovDireccione>();
}
