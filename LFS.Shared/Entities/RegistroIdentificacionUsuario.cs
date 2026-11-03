using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LFS.Shared.Entities
{
  public class RegistroIdentificacionUsuario
  {
    [Key]
    public int Id { get; set; }

    [Required]
    public int IdMaquinaDispensacion { get; set; }

    [Required]
    public int IdMasterSubCliente { get; set; }

    [Required]
    public DateTime Fecha { get; set; }

    [ForeignKey("IdMasterSubCliente")]
    public virtual MasterSubClientes MasterSubClientes { get; set; }

    [ForeignKey("IdMaquinaDispensacion")]
    public virtual MasterMaquinaDispensacion MasterMaquinaDispensacion { get; set; }
  }
}
