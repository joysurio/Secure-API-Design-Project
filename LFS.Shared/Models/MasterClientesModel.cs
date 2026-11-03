using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LFS.Shared.Models
{
  public class MasterClientesModel
  {
    public int Id { get; set; }

    public string Nombre { get; set; }

    public string RazonSocial { get; set; }

    public int? IdContactoPrincipal { get; set; }

    public string Cif { get; set; }

    public short? Iva { get; set; }

    public DateTime? FechaAlta { get; set; }

    public DateTime? FechaBaja { get; set; }

    public string DenominacionSubCliente { get; set; }

    public int IdEtiquetaDefecto { get; set; }

    public bool Borrado { get; set; }

    public string Actividad { get; set; }

    public string TextoEtiquetaPrenda { get; set; }

    public int? IdClienteLavander { get; set; }

    public int? TipoMovimientoAlbaran { get; set; }

    public int? IdActividad { get; set; }

    public int TipoMarcadoPrenda { get; set; }

    public int CantidadTaquillas { get; set; }

    public int? NumLlavesPorTaquilla { get; set; }

    public bool OrdenacionSoplsoloConPosicionesFijas { get; set; }

    public bool NoEliminarCampo2AlAsociarTarjeta { get; set; }

    public short? DiaDeSemanaParaServicio { get; set; }

    public bool MaquinasRfid { get; set; }

    public int? TipoMovimientoAumentaCiclosPrenda { get; set; }

    public bool CrearPedidoEnEntrada { get; set; }
  }
}
