using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LFS.Shared.ViewModels
{
    public class UltimoMovimientoPrendaViewModel
    {
        public int IdPrenda { get; set; }
        public string Descrip { get; set; }
        public int Norden { get; set; }
        public int NTotal { get; set; }
        public string CodigoTAG { get; set; }
        public string Modelo { get; set; }
        public string Talla { get; set; }
        public string TipoMov { get; set; }
        public int IdTipoMov { get; set; }
        public int IdMov { get; set; }
        public string Obser { get; set; }
        public DateTime Fecha { get; set; }
        public string Nombre { get; set; }
        public string Apellido1 { get; set; }
        public string Apellido2 { get; set; }
        public string NombreCliente { get; set; }
    }
}
