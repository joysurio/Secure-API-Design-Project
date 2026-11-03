using System;
using System.ComponentModel.DataAnnotations;
using System.Security.Permissions;

namespace LFS.Shared.Entities.EntitiesOffline
{
    public class MasterMaquinaDispensacion
    {
        public int Id { get; set; }
        [Required, MaxLength(50)]
        public string Descripcion { get; set; }
        public int? IdCliente { get; set; }
        public int? IdTipoMaquina { get; set; }
        public int? PuertoPLC { get; set; }
        public int? PuertoLectorTarjetas { get; set; }
        public int? PuertoPLC2 { get; set; }
        public int? PuertoVisorMaquinaSucio { get; set; }
        [MaxLength(50)]
        public string? IdentificadorLogo { get; set; }
        public int? IdPuesto { get; set; }
        public bool PedirCodigoNuevoUsuario { get; set; }
        public int? CantHuecos { get; set; }
        [MaxLength(100)]
        public string? CtoServTecnico { get; set; }
        public bool CodigoManual { get; set; }
        [MaxLength(100)]
        public string? IPLector { get; set; }
        public int? TipoAntenaUHF { get; set; }
        public DateTime? HoraReposicion { get; set; }
        [MaxLength(200)]
        public string? Observaciones { get; set; }
        public string? emailAviso { get; set; }
        public int? IdAlmacenCentral { get; set; }
        public int? IdModeloDftIzq { get; set; }
        public int? IdModeloDftDer { get; set; }

        [MaxLength(20)]
        public string? ResolucionPantalla { get; set; }

    }
}
