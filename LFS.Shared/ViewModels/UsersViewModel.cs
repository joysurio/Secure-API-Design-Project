using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LFS.Shared.ViewModels
{
    public class UsersViewModel
    {
        public int IdItemUsers { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string NombreCompleto { get; set; }
        public string Dni { get; set; }
        public string NumeroEmpleado { get; set; }
        public string Campo1 { get; set; }
        public string Departamento { get; set; }
        public IEnumerable<SelectListItem> Users { get; set; }
    }
}
