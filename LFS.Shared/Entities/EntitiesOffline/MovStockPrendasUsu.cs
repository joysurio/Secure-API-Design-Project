using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LFS.Shared.Entities.EntitiesOffline
{

  public partial class MovStockPrendasUsu
  {
    public int Id { get; set; }

    public int IdSubCliente { get; set; }

    public int IdModelo { get; set; }

    [MaxLength(50)]
    public string? Talla { get; set; }

    public int Cantidad { get; set; }

    [ForeignKey("IdModelo")]
    public virtual MasterModeloPrenda IdModeloNavigation { get; set; }

    [ForeignKey("IdSubCliente")]
    public virtual MasterSubClientes IdSubClienteNavigation { get; set; }
  }
}