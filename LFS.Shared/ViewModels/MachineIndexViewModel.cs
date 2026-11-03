using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LFS.Shared.ViewModels
{
    public class MachineIndexViewModel
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public int? IdCliente { get; set; }
        public string Cliente { get; set; }
        public int IdTipoMaquina { get; set; }
        public string TipoMaquina { get; set; }
    }
}