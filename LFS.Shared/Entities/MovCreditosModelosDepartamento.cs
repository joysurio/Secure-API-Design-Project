using System;
using System.Collections.Generic;

namespace LFS.Shared.Entities;

public partial class MovCreditosModelosDepartamento
{
    public int IdDepartamento { get; set; }

    public int IdModelo { get; set; }

    public int TipoRango { get; set; }

    public int LimiteCantidadPrendasEnUso { get; set; }

    public virtual MasterDepartamentosCliente IdDepartamentoNavigation { get; set; }

    public virtual MasterModeloPrenda IdModeloNavigation { get; set; }
}
