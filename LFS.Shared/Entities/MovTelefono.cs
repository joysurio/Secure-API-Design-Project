using System;
using System.Collections.Generic;

namespace LFS.Shared.Entities;

public partial class MovTelefono
{
    public int Id { get; set; }

    public int? IdFormaContacto { get; set; }

    public string Descripcion { get; set; }

    public string Valor { get; set; }

    public string Contacto { get; set; }

    public virtual MasterFormasContacto IdFormaContactoNavigation { get; set; }

    public virtual ICollection<MovTelefonosCliente> MovTelefonosClientes { get; set; } = new List<MovTelefonosCliente>();

    public virtual ICollection<MovTelefonosPropio> MovTelefonosPropios { get; set; } = new List<MovTelefonosPropio>();

    public virtual ICollection<MovTelefonosProveedore> MovTelefonosProveedores { get; set; } = new List<MovTelefonosProveedore>();
}
