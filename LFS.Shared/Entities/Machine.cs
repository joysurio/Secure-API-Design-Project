using System;
using System.ComponentModel.DataAnnotations;

namespace LFS.Shared.Entities
{
    public class Machine
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
        public int MachineModelId { get; set; }
        public MachineModel MachineModel { get; set; }
        public bool Deleted { get; set; }

        public int? PuertoPLC { get; set; }
        public int? PuertoLectorTarjetas { get; set; }
        public int? PuertoPLC2 { get; set; }
        public int? PuertoVisorMaquinaSucio { get; set; }
        public string IdentificadorLogo { get; set; }
        public int? idPuesto { get; set; }
        public bool? PedirCodigoNuevoUsuario { get; set; }
        public int? CantidadHuecos { get; set; }
        public bool? HabilitarIdentificacionManual { get; set; }
        public string IPLEctor { get; set; }
        public int? TipoAntenaUHF { get; set; }
        public string Observaciones { get; set; }
        public string ResolucionPantalla { get; set; }
        public bool? SiempreMovimientoSalidaEnLenceria { get; set; }
        public bool? LecturaTagPantallaPrincipalLenceria { get; set; }
        public bool? VerBotonNuevaPrendaLenceria { get; set; }
        public bool? VerBotonTaquillasEnLencria { get; set; }
        public bool? MostrarMensajeCredito { get; set; }
        public bool? PermitirRetornarUsuariosBaja { get; set; }
        public int? IdCliente { get; set; }
    }
}
