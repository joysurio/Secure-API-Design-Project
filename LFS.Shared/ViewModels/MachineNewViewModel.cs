using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LFS.Shared.ViewModels
{
  public class MachineNewViewModel
  {
    public int Id { get; set; }
    public string Descripcion { get; set; }
    public int IdCliente { get; set; }
    public int IdTipoMaquina { get; set; }
    public int PuertoPLC { get; set; }
    public int PuertoLectorTarjetas { get; set; }
    public int PuertoPLC2 { get; set; }
    public string IdentificadorLogo { get; set; }
    public int IdPuesto { get; set; }
    public bool PedirCodigoNuevoUsuario { get; set; }
    public int CtoServTecnico { get; set; }
    public bool CodigoManual { get; set; }
    public string IPLector { get; set; }
    public int TipoAntenaUHF { get; set; }
    public DateTime HoraReposicion { get; set; }
    public string Observaciones { get; set; }
    public string EmailAviso { get; set; }
    public int IdAlmacenCentral { get; set; }
    public string ResolucionPantalla { get; set; }
    public int IdModeloDfrDer { get; set; }
    public int IdModeloDfrIzq { get; set; }
    public bool? EsDoblePuerta { get; set; }
    public int NPueta { get; set; }
    public int PosXDispensacion { get; set; }
    public int PosYDispensacion { get; set; }
    public int PosXReposo { get; set; }
    public int PuertoLectorTarjetasPD { get; set; }
    public string IPLectorPD { get; set; }
  }
}