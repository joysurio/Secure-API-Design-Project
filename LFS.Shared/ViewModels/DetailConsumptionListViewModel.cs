using System;

namespace LFS.Shared.ViewModels
{
    public class DetailConsumptionListViewModel
    {
		public int IdMov { get; set; }
		public int IdMaquina { get; set; }
		public string Maquina { get; set; }
		public int IdCliente { get; set; }
		public string Cliente { get; set; }
		public int IdUsuario { get; set; }
		public string NombreUsuario { get; set; }
		public int IdModelo { get; set; }
		public string Modelo { get; set; }
		public string Talla { get; set; }
		public DateTime Fecha { get; set; }
		public int IdTipoMovimiento { get; set; }
		public string TipoMovimiento { get; set; }
		public string Departamento { get; set; }
	}

}
