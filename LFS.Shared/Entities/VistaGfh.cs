using System;
using System.Collections.Generic;

namespace LFS.Shared.Entities;

public partial class VistaGfh
{
    public DateTime? Fecha { get; set; }

    public string Gfh { get; set; }

    public string Descripcion { get; set; }

    public int Id { get; set; }

    public string Apellido1 { get; set; }

    public string Apellido2 { get; set; }

    public string Nombre { get; set; }

    public string Descrip { get; set; }

    public int? CuentaDeId { get; set; }
}
