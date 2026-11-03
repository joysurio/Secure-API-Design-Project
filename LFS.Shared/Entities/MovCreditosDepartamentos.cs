using System;
using System.Collections.Generic;

namespace LFS.Shared.Entities;

public partial class MovCreditosDepartamentos
{
    public int Id { get; set; }

    public int IdDepartamento { get; set; }

    public int? CantMaxPrendasUso { get; set; }

    public int? LimitacionHoras { get; set; }

    public int RangoCredito { get; set; }

    public virtual MasterDepartamentosCliente IdDepartamentoNavigation { get; set; }
}
