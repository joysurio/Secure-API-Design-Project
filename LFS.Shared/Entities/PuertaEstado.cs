using System;
using System.Collections.Generic;

namespace LFS.Shared.Entities
{
    public partial class PuertaEstado
    {
        public int Id { get; set; } // Nueva propiedad Id

        public int IdMaquina { get; set; }

        public int? Npuerta { get; set; }

        public int? EstadoMaquina { get; set; }

        public bool? PrendaEnCajon { get; set; }

        public bool? PuertaAbierta { get; set; }

        public bool? SensorPinza { get; set; }
    }
}
