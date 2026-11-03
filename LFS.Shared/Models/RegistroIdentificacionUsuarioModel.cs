using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LFS.Shared.Models
{
  public class RegistroIdentificacionUsuarioModel
  {
    
    public int Id { get; set; }
    
    public int IdMaquinaDispensacion { get; set; }
    
    public int IdMasterSubCliente { get; set; }
    
    public DateTime Fecha { get; set; }
  }
}
