using System;
using System.Collections.Generic;
using System.Text;

namespace LFS.Shared.Models.Old
{
    public class GarmentsInUse
    {
        public int Id { get; set; }
        public int? IdModeloPrenda { get; set; }
        public int? IdCliente { get; set; }
        public int? IdSubCliente { get; set; }
        public int? Propietario { get; set; }
        public string CodigoTag { get; set; }
        public string Descrip { get; set; }
        public DateTime? FAlta { get; set; }
        public DateTime? FBaja { get; set; }
        public bool? Nueva { get; set; }
        public int? NLavados { get; set; }
        public string Taquilla { get; set; }
        public string Color { get; set; }
        public bool? Accesorio { get; set; }
        public string Talla { get; set; }
        public bool? Borrado { get; set; }
        public int? NCiclos { get; set; }
        public int? NOrden { get; set; }
        public int? NTotal { get; set; }
        public int? LastMov { get; set; }
        public DateTime? DateLastMov { get; set; }
        public DateTime? FBajaReal { get; set; }
        public int? Stock { get; set; }
        public bool? Facturable { get; set; }
        public int? IdMaqLastMov { get; set; }
        public string DescripcionModelo { get; set; }
        public string NombreUsuario { get; set; }
        public bool TieneMovimientos { get; set; }
        public bool Personalizada { get; set; }
        public string UltimoMovimiento { get; set; }
        public int EnUso { get; set; }
        public int RegistroMovimientoTransitorio { get; set; }
        public int? IdtipoRopa { get; set; }
        public double? TemperaturaLavadoRopa { get; set; }
        public double? TemperaturaSecadoRopa { get; set; }
    }

}
