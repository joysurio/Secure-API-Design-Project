using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LFS.Shared.ViewModels
{
    public class MovMaquinaDisAlmacenesViewModel
    {
        public int Id { get; set; }
        public string IdMaquinaDispensacion { get; set; }
        public int IdAlmacen { get; set; }
        public string Talla { get; set; }
        public int PosEjeX { get; set; }
        public int PosEjeY { get; set; }
        public int CantMax { get; set; }
        public int StockReal { get; set; }
        public int? IdModelo { get; set; }
        public int? Antena { get; set; }
        public int? CantMin { get; set; }
        public int CantidadPrendaDispensada { get; set; }
        public bool? CargarPrendasFijas { get; set; }
        public string CodigoTag { get; set; }
        public int? IdPrenda { get; set; }
        public int MayorBajadoPermitido { get; set; }
        public int? IdPrenda2 { get; set; }
        public int? IdModelo2 { get; set; }
        public string Talla2 { get; set; }
        public int PuertoPlccerradura { get; set; }
        public int PuertoPuertaAbierta { get; set; }
    }
}