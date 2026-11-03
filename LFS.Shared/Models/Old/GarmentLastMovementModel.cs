using LFS.Shared.Entities;

namespace LFS.Shared.Models.Old
{
    public class GarmentLastMovementModel
    {
        public int? IdSubClienteUsaPrenda { get; set; }
        public bool RegistroMovimientoTransitorio { get; set; }
        public bool EnUso { get; set; }
        public MasterPrendas MasterPrendas { get; set; }
    }
}
