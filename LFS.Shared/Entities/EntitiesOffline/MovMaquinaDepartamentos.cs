using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LFS.Shared.Entities.EntitiesOffline;

public partial class MovMaquinaDepartamentos
{
  [Key]
  public int Id { get; set; }

  [Required]
  public int IdMasterMaquinaDispensacion { get; set; }

  [Required]
  public int IdMasterDepartamentosCliente { get; set; }

  [ForeignKey("IdMasterMaquinaDispensacion")]
  public virtual MasterMaquinaDispensacion MasterMaquinaDispensacion { get; set; }

  [ForeignKey("IdMasterDepartamentosCliente")]
  public virtual MasterDepartamentosCliente MasterDepartamentosCliente { get; set; }
}
