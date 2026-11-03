using System.ComponentModel.DataAnnotations;

namespace LFS.SecureAPI.Models
{
    public class CreditsRequest
    {
        [Required]
        public int IdDepartamento { get; set; }

        public int? CantMaxPrendasUso { get; set; }

        public int? LimitacionHoras { get; set; }

        [Required]
        public int RangoCredito { get; set; }
    }
}
