using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LFS.Shared.Models
{
    public class PrendaDisponibleEnLenceria
    {
        public string Descripcion { get; set; }

        public int idModelo { get; set; }

        public int Total { get; set; }

        public int EnUbicacion { get; set; }

        public int Resto { get; set; }

        public int Credito { get; set; }

        public int EnUso { get; set;}

        public string Talla { get; set; }
    }
}
