using System;

namespace LFS.Shared.ViewModels
{
    public class MachineLoadsViewModel
    {
        public int Id { get; set; }
        public int IdMaquina { get; set; }
        public int IdAlmacen { get; set; }
        public int IdModelo { get; set; }
        public string ModeloPrenda { get; set; }
        public string Usuario { get; set; }
        public string Talla { get; set; }
        public string Maquina { get; set; }
        public DateTime Fecha { get; set; }
        public int CantidadEnMaquina { get; set; }
        public int CantidadRepuesta { get; set; }
        public int Cantidad { get; set; }
        public int IdEmpleado { get; set; }
        public string Empleado { get; set; }
    }
}
