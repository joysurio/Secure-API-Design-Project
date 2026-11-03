using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LFS.Shared.ViewModels
{
    public class GarmentsInUseViewModel
    {
        public int IdUsuario { get; set; }
        public string Nombre { get; set; }
        public string Apellido1 { get; set; }
        public string Apellido2 { get; set; }
        public int IdDepartamento { get; set; }
        public string Departamento { get; set; }
        public int IdModeloPrenda { get; set; }
        public string ModeloPrenda { get; set; }
        public int TopePrendas { get; set; }
        public int PrendasEnUso { get; set; }
        public int PrendasAsignadas { get; set; }
        public int CantMaxPrendasUso { get; set; }
        public bool RopaPersonalizada { get; set; }
    }
}
