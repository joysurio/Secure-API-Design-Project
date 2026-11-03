using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LFS.Shared.Entities
{
  public class MasterNotificaciones
  {
    [Key]
    public int Id { get; set; }

    [Required]
    public int IdMasterAviso { get; set; }

    [Required]
    public DateTime Fecha { get; set; }

    [Required]
    public bool AvisoEnviado { get; set; } = false;

    [Required]
    public bool NotificacionMostrada { get; set; } = false;

    [Required]
    [StringLength(1000)]
    public string Descripcion { get; set; }

    [Required]
    public int IdMaquina { get; set; }

    
    [ForeignKey("IdMasterAviso")]
    public virtual MasterAvisosPorEmail MasterAvisosPorEmail { get; set; }

    [ForeignKey("IdMaquina")]
    public virtual MasterMaquinaDispensacion MasterMaquinaDispensacion { get; set; }
  }
}
