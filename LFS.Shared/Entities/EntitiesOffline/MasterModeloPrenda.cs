using System.ComponentModel.DataAnnotations;

namespace LFS.Shared.Entities.EntitiesOffline
{
    public class MasterModeloPrenda
    {
        public int Id { get; set; }
        public int? Id_TipoPrenda { get; set; }
        [MaxLength(50)]
        public string? Descripcion { get; set; }
        public int? MaxLavados { get; set; }
        public int? Id_Proveedor { get; set; }
        public int TipoPosicionSOPL { get; set; }
        public bool NoRetornableMaquinaSucio { get; set; }
        public int? ModeloComodin { get; set; }
        public int? IdTipoRopa { get; set; }
        public int? TemperaturaLavadoRopa { get; set; }
        public int? TemperaturaSecadoRopa { get; set; }
    }
}
