using System;
using System.Collections.Generic;

namespace LFS.Shared.Entities.EntitiesOffline
{

    public partial class MaquinasDispensadorasParametros
    {
        public int Id { get; set; }

        public int IdMaquina { get; set; }

        public bool SiempreMovimientoSalida { get; set; }

        public bool LecturaTagpantallaPrincipalLenceria { get; set; }

        public bool VerBotonNuevaPrendaLenceria { get; set; }

        public bool VerTaquillas { get; set; }

        public bool MostrarMensajeCredito { get; set; }

        public bool? PermitirRetornarUsuariosBaja { get; set; }

        public bool MostrarModelosPrendasRecientes { get; set; }

        public virtual MasterMaquinaDispensacion? IdMaquinaNavigation { get; set; }
    }
}
