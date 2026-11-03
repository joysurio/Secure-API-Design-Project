using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LFS.Shared.ViewModels
{
    public class LoadUserIndexViewModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }

        public string Usuario { get; set; }

        public bool? Activo { get; set; }

        public string TipoUsuario { get; set; }

        public string CodigoTAG { get; set; }

    }
}