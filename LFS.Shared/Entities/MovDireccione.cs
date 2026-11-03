using System;
using System.Collections.Generic;

namespace LFS.Shared.Entities;

public partial class MovDireccione
{
    public int Id { get; set; }

    public int? IdTipoDireccion { get; set; }

    public string Descripcion { get; set; }

    public int? IdViapublica { get; set; }

    public string Calle { get; set; }

    public string Edificio { get; set; }

    public string Portal { get; set; }

    public string Escalera { get; set; }

    public string Piso { get; set; }

    public string Puerta { get; set; }

    public string RestoDireccion { get; set; }

    public string Cp { get; set; }

    public string Localidad { get; set; }

    public int? IdProvincia { get; set; }

    public virtual MasterProvincia IdProvinciaNavigation { get; set; }

    public virtual MasterTiposDireccione IdTipoDireccionNavigation { get; set; }

    public virtual MasterViasPublica IdViapublicaNavigation { get; set; }

    public virtual ICollection<MovDireccionesCliente> MovDireccionesClientes { get; set; } = new List<MovDireccionesCliente>();

    public virtual ICollection<MovDireccionesPropio> MovDireccionesPropios { get; set; } = new List<MovDireccionesPropio>();

    public virtual ICollection<MovDireccionesProveedore> MovDireccionesProveedores { get; set; } = new List<MovDireccionesProveedore>();
}
