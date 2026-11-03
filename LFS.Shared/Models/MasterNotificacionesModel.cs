using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LFS.Shared.Models
{
  public class MasterNotificacionesModel
  {
    public int Id { get; set; }
    public int IdMasterAviso { get; set; }
    public DateTime Fecha { get; set; }
    public bool AvisoEnviado { get; set; }
    public bool NotificacionMostrada { get; set; }    
    public string Descripcion { get; set; }
    public int IdMaquina { get; set; }
    public string NombreAviso { get; set; }
    public string DescripcionMaquina { get; set; }
  }
}
