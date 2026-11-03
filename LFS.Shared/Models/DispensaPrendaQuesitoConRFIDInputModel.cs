using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LFS.Shared.Models
{
    public class DispensaPrendaQuesitoConRFIDInputModel
    {
        public int Id { get; set; }
        public int IdDispensador { get; set; }
        public int IdAlmacen { get; set; }
        public int IdSubCliente { get; set; }
        public int TipoMov { get; set; } = 51;
        public bool EsAlmacenDoble { get; set; }
        public bool ModoOffline { get; set; }
        public int IdPrenda1 { get; set; }
        public int IdPrenda2 { get; set; }
        public int IdModelo1 { get; set; }
        public int IdModelo2 { get; set; }
        public string Talla1 { get; set; }
        public string Talla2 { get; set; }
        public int CantidadPrendasMaxima { get; set; }
        public string DescripcionMaquinaDispensacion { get; set; }
        public string IdiomaPorDefecto { get; set; }
    }
}
