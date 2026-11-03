using System.ComponentModel.DataAnnotations;

namespace LFS.SecureAPI.Models
{
    public class MachineLoadRequest
    {
        [Required]
        [Range(1, 2)]
        public int TipoMov { get; set; } // 1 = carga, 2 = descarga

        [Required]
        public int UserId { get; set; }
    }
}
