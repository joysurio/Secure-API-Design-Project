using System;
using System.Collections.Generic;

namespace LFS.Shared.Entities;

public partial class MovReposicione
{
    public Guid IdRep { get; set; }

    public int PuntoRep { get; set; }

    public int ModeloRep { get; set; }

    public int? CantidadRep { get; set; }

    public DateTime FechaRep { get; set; }

    public int? UsuarioRep { get; set; }

    public int PuestoRep { get; set; }

    public int? SolicitadasRep { get; set; }

    public bool RepuestoRep { get; set; }

    public DateTime? FechaEntregaRep { get; set; }

    public int? PactoRep { get; set; }

    public int? ContadasRep { get; set; }

    public int? AlmacenRep { get; set; }

    public DateTime? LastEditDate { get; set; }

    public DateTime? CreationDate { get; set; }

    public int? TipoMovRep { get; set; }

    public string ObservacionesRep { get; set; }

    public int? TipoReposicionRep { get; set; }
}
