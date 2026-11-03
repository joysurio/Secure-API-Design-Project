using System;

namespace Raul.Shared.Entities
{
    public partial class Mov_HistoricoGestionTaquillas
    {
        public int IdSubCliente { get; set; }
        public int IdTaquilla { get; set; }
        public int IdEstado { get; set; }
        public DateTime? Fecha { get; set; }
    }
}
