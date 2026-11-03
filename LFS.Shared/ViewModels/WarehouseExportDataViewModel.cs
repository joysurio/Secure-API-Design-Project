using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LFS.Shared.ViewModels
{
    public class WarehouseExportDataViewModel
	{
        public string Maquina { get; set; }
		public int IdMaquina { get; set; }
		public int IdAlmacen { get; set; }
		public string Modelo { get; set; }
		public int IdModelo { get; set; }
		public string Talla { get; set; }
		public int PosEjeX { get; set; }
		public int PosEjeY { get; set; }
		public int Cantidad { get; set; }
        public int StockReal { get; set; }
        public string PuertoPlccerradura { get; set; }
        public string PuertoPuertaAbierta { get; set; }
        public int? MayorBajadoPermitido { get; set; }
        public string Modelo2 { get; set; }
		public int IdModelo2 { get; set; }
		public string Talla2 { get; set; }
		public string Prenda { get; set; }
		public int IdPrenda { get; set; }
		public string Prenda2 { get; set; }
		public int IdPrenda2 { get; set; }
		public bool? CargarPrendasFijas { get; set; }

    }
}