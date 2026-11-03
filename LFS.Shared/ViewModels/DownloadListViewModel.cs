using System;

namespace LFS.Shared.ViewModels
{
    public class DownloadListViewModel
	{
        public int Id { get; set; }
        public string ModeloPrenda { get; set; }
        public string Usuario { get; set; }
        public string Talla { get; set; }
        public string Maquina { get; set; }
        public DateTime FechaMovPrenda { get; set; }
        public DateTime FechaCargaDescarga { get; set; }
        public int Cantidad { get; set; }
		

	}

}
