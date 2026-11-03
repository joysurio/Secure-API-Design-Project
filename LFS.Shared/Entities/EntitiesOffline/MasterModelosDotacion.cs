using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LFS.Shared.Entities.EntitiesOffline
{

    public partial class MasterModelosDotacion
    {
        public int Id { get; set; }

        [MaxLength(50)]
        public string? Descripcion { get; set; }

        public int IdModelo { get; set; }

        public int IdDotacion { get; set; }

        public int CantPrendas { get; set; }

        public bool CrearSerie { get; set; }

        public DateTime? Fbaja { get; set; }

        public int? Nlavados { get; set; }

        [MaxLength(50)]
        public string? Color { get; set; }

        public bool? Accesorio { get; set; }

        public bool? Nueva { get; set; }

        public bool? Facturable { get; set; }

        public int? IdEtiqueta { get; set; }

        public bool EsServicioSemanal { get; set; }

        public virtual MasterDotaciones? IdDotacionNavigation { get; set; }

        public virtual MasterModeloPrenda? IdModeloNavigation { get; set; }
    }
}