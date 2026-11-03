using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LFS.Shared.Models
{
  public class TaquillaModel
  {
    public int Id { get; set; }
    public string Descripcion { get; set; }   
    public int? TipoVestuario { get; set; }    
    public int? IdEstadoTaquilla { get; set; }
    public string EstadoTaquilla { get; set; }
    public int? IdEstadoLlave1 { get; set; }    
    public int? IdEstadoLlave2 { get; set; }    
    public string Observaciones { get; set; }
    public int? Usuario { get; set; }
    public string Campo1 { get; set; }
    public string Campo2 { get; set; }
    public int? Cliente { get; set; }
    public DateTime? DateLastMov { get; set; }
    public string NombreCliente { get; set; }
    public string Vestuario { get; set; }
    public string EstadoLlave1 { get; set; }
    public string EstadoLlave2 { get; set; }
    public int? IdEdificio { get; set; }
    public int IdVestuario { get; set; }
  }
}
