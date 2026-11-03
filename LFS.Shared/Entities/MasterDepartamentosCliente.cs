using System;
using System.Collections.Generic;

namespace LFS.Shared.Entities;

public partial class MasterDepartamentosCliente
{
  public int Id { get; set; }

  public int? IdCliente { get; set; }

  public string Descripcion { get; set; }

  public virtual MasterClientes IdClienteNavigation { get; set; }

  public virtual ICollection<MasterSubClientes> MasterSubClientes { get; set; } = new List<MasterSubClientes>();

  public virtual ICollection<MovCreditosDepartamentos> MovCreditosDepartamentos { get; set; } = new List<MovCreditosDepartamentos>();

  // Relación Muchos a Muchos
  public virtual ICollection<MovMaquinaDepartamentos> MovMaquinaDepartamentos { get; set; }

}
