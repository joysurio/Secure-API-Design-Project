using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LFS.Shared.ViewModels
{
    public class MachinePositionsViewModel
    {
        public int Id { get; set; }
        public int IdAlmacen { get; set; }
        public int IdMaquina { get; set; }
        public string Maquina { get; set; }
        public string Modelo { get; set; }
        public int? IdModelo { get; set; }
        public int PosEjeX { get; set; }
        public int PosEjeY { get; set; }
        public int? IdCliente { get; set; }
        public string Cliente { get; set; }
        public string Talla { get; set; }
    }
}