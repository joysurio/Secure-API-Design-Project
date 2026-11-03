using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace LFS.Shared.Entities.EntitiesOffline;

public partial class MovModSubCliente
{
  public int Id { get; set; }

  public int id_ModeloPrenda { get; set; }

  [ForeignKey(nameof(id_ModeloPrenda))]
  public virtual MasterModeloPrenda IdModeloPrendaNavigation { get; set; }

  public int id_SubCliente { get; set; }

  [ForeignKey(nameof(id_SubCliente))]
  public virtual MasterSubClientes IdSubClienteNavigation { get; set; }
}
