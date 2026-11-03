using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LFS.Shared.ViewModels
{
  public class RegistroIdentificacionUsuarioModelViewModel
  {
    public int Id { get; set; }

    public int IdMaquinaDispensacion { get; set; }

    public int IdMasterSubCliente { get; set; }

    public DateTime Fecha { get; set; }

    public string NombreUsuario { get; set; }
    public string Maquina { get; set; }
  }
}
