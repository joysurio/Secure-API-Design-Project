using System.ComponentModel.DataAnnotations;

namespace LFS.Shared.Models
{
    public class Paciente
    {
        public string numeroPaciente { get; set; }
        public string cama { get; set; }
        public string unidad { get; set; }
        public string nombrePaciente { get; set; }
        public int? cantidadMedicamento { get; set; }
        public string medicamento { get; set; }

    }
}
