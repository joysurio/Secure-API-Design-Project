using System;
using System.Collections.Generic;

namespace LFS.Shared.Entities;

public partial class MasterFormasContacto
{
    public int Id { get; set; }

    public string Denominacion { get; set; }

    /// <summary>
    /// -1=Ninguno;1=TelÃ©fono,2=movil,3=fax,4=email (0=def por usuario)
    /// </summary>
    public int? Tipo { get; set; }

    public bool? System { get; set; }

    public virtual ICollection<MovTelefono> MovTelefonos { get; set; } = new List<MovTelefono>();
}
