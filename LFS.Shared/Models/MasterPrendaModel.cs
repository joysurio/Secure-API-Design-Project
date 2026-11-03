using LFS.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LFS.Shared.Models
{
  public class MasterPrendaModel
  {
    public int Id { get; set; }
    
    public int? IdModeloPrenda { get; set; }
   
    public int? IdCliente { get; set; }
    
    public int? IdSubCliente { get; set; }

    public string CodigoTag { get; set; }

    public string Descrip { get; set; }

    public DateTime? Falta { get; set; }

    public DateTime? Fbaja { get; set; }

    public bool? Nueva { get; set; }

    public int? Nlavados { get; set; }

    public string Taquilla { get; set; }

    public string Color { get; set; }

    public bool? Accesorio { get; set; }

    public string Talla { get; set; }

    public int? IdEtiqueta { get; set; }

    public bool? Borrado { get; set; }

    public int? Nciclos { get; set; }

    public int? Norden { get; set; }

    public int? Ntotal { get; set; }

    public int? LastMov { get; set; }

    public DateTime? DateLastMov { get; set; }

    public DateTime? FbajaReal { get; set; }

    public int? Stock { get; set; }

    public int? CantidadUso { get; set; }

    public bool? Facturable { get; set; }

    public int? IdMaqLastMov { get; set; }

    public bool Personalizada { get; set; }

    public int? LastIdMov { get; set; }

    public int? ComprobacionLecturas { get; set; }

    public int? IdUbicacion { get; set; }

    public int? IdTipoRopa { get; set; }

    public int? Id_TipoPrenda { get; set; }

    public int TipoPosicionSOPL { get; set; }

    public double? TemperaturaLavadoRopa { get; set; }

    public double? TemperaturaSecadoRopa { get; set; }

    public int ModeloComprobacion { get; set; }

    public bool? NoActualizarModeloTalla { get; set; }

    public bool NoRetornableMaquinaSucio { get; set; }

    public int? ModeloComodin { get; set; }

    public virtual MasterClientes Cliente { get; set; } 

    public virtual MasterModeloPrenda ModeloPrenda { get; set; } 

    public virtual MasterSubClientes Usuario { get; set; }

  }
}
