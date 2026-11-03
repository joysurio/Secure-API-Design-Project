using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LFS.Shared.Models
{
  public class PuestoLecturaModel
  {
    public int Id { get; set; }
    public int? TipoAntenaID { get; set; }
    public string TipoAntenaNombre { get; set; }
    public int? TipoAntenaUltimo { get; set; }
    public string ComPort { get; set; }
    public int? ModeloAntena { get; set; }
    public string ModeloAntenaNombre { get; set; }
    public bool? Leds { get; set; }
    public int? LedsColumnas { get; set; }
    public int? LedsFilas { get; set; }
    public int? IdEmpresa { get; set; }
    public bool? PedirUbicacion { get; set; }
    public bool? MostrarEnPDA { get; set; }
    public bool? Mostrar { get; set; }
    public bool NoBorrar { get; set; }
    public bool EsBaja { get; set; }

  }
}
