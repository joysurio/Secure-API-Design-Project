using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace LFS.Shared.Entities.EntitiesOffline
{

  public partial class MovCreditosDepartamentos
  {
    public int Id { get; set; }

    public int IdDepartamento { get; set; }

    public int? CantMaxPrendasUso { get; set; }

    public int? LimitacionHoras { get; set; }

    public int RangoCredito { get; set; }

    [ForeignKey("IdDepartamento")]
    public virtual MasterDepartamentosCliente? IdDepartamentoNavigation { get; set; }
  }
}

