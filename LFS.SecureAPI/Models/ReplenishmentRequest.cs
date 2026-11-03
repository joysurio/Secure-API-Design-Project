using System.ComponentModel.DataAnnotations;

namespace LFS.SecureAPI.Models
{
    public class ReplenishmentRequest
    {
        [Required]
        public int PuntoRep { get; set; }

        [Required]
        public int ModeloRep { get; set; }

        public int? CantidadRep { get; set; }

        public int? UsuarioRep { get; set; }

        [Required]
        public int PuestoRep { get; set; }

        public int? SolicitadasRep { get; set; }
    }
}
