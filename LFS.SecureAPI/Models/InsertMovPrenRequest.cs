using System.ComponentModel.DataAnnotations;

namespace LFS.SecureAPI.Models
{
    public class InsertMovPrenRequest
    {
        [Required]
        [MaxLength(50)]
        public string CodigoTAG { get; set; } = string.Empty;

        [Required]
        public int IdPuesto { get; set; }

        [Required]
        public int IdTipoAntena { get; set; }

        [Required]
        public int IdCli { get; set; }

        [Required]
        public int IdModeloPrenda { get; set; }

        [MaxLength(50)]
        public string? Talla { get; set; }
    }
}
