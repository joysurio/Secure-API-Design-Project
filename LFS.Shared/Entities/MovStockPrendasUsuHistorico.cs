using System;
using System.Collections.Generic;

namespace LFS.Shared.Entities
{
    public partial class MovStockPrendasUsuHistorico
    {
        public int Id { get; set; } // Nueva propiedad para la clave primaria

        public DateTime Fecha { get; set; }

        public int IdSubCliente { get; set; }

        public int IdModelo { get; set; }

        public int StockEnUsoActual { get; set; }

        public int? IdLastMov { get; set; }

        public int? IdUsuActualizaMov { get; set; }

        public int? IdMaquinaMov { get; set; }

        public string UsuarioOperacion { get; set; }
    }
}
