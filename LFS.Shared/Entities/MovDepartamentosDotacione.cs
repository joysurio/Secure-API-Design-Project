using System;
using System.Collections.Generic;

namespace LFS.Shared.Entities;

public partial class MovDepartamentosDotacione
{
    public int IdDepartamento { get; set; }

    public int IdDotacion { get; set; }

    public virtual MasterDepartamentosCliente IdDepartamentoNavigation { get; set; }

    public virtual MasterDotacione IdDotacionNavigation { get; set; }
}
