using System;

namespace Raul.Shared.Entities
{
    public partial class Mov_SubClientesGestionTaquillas
    {
        public int IdSubCliente { get; set; }
        public DateTime? FechaPrevistaBaja { get; set; }
        public DateTime? FechaPrevistaAlta { get; set; }
    }
}
