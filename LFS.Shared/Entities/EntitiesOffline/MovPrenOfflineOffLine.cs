using System;
using System.ComponentModel.DataAnnotations;

namespace LFS.Shared.Entities.EntitiesOffline
{
    public class MovPrenOffline : MovPrenBase
    {
        public bool Actualizado { get; set; }
        public string IdAlmacen { get; set; }
    }
    public class MovPrenBase
    {
        public int Id { get; set; }
        public int? IdPrenda { get; set; }        
        public DateTime? Fecha { get; set; }
        public int? IdTipoAntena { get; set; }
        public int? IdSubCli { get; set; }
        public int? IdModeloPrenda { get; set; }       
        [MaxLength(50)]
        public string Talla { get; set; }
    }
}
