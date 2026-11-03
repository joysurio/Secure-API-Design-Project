using LFS.Shared.Enums;
using LFS.Shared.Models.Old;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LFS.Shared.Models
{
    public class InsertarPrendaInputModel
    {
        public int IdModelo { get; set; }
        public string StrModelo { get; set; }
        public string Talla { get; set; }
        public string CodigoTag { get; set; }
        public bool Nueva { get; set; }
        public Person UsuarioPrenda { get; set; }
        public bool AsignarPrendaUsuarioGenerico { get; set; }
        public byte[] FotoPrenda { get; set; }
        public EnumTipoMarcadoPrendas TipoMarcadoPrendas { get; set; }
        public int? IdTipoRopa { get; set; }      
        public int IdClienteMaquina { get; set; }
        public int IdMaquina { get; set; }
        public bool NoActualizarModeloTalla { get; set; }
    }
}
