using LFS.Shared.Enums;
using LFS.Shared.Models.Old;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LFS.Shared.Models
{
  public class PrendaLeidaModel
  {
    public GarmentRFModel Prenda { get; set; }
    public int IdUsuarioPrendaFijado { get; set; }
    public int TipoPuntoLectura { get; set; }

    public PrendaLeidaModel()
    {

    }

    public ResultadoCheckPrendaLeida CheckPrenda()
    {
      if (Prenda.Propietario.Fbaja != null && Prenda.Propietario.Fbaja < DateTime.Now)
      {
        return ResultadoCheckPrendaLeida.USUARIO_SUPERADO_FECHA_BAJA;
      }

      if ((bool)Prenda.Propietario.Borrado)
      {
        return ResultadoCheckPrendaLeida.USUARIO_BORRADO;
      }

      if (Prenda.Borrada)
      {
        return ResultadoCheckPrendaLeida.PRENDA_BORRADA;
      }

      if (IdUsuarioPrendaFijado != Prenda.Propietario.Id && IdUsuarioPrendaFijado > 0)
      {
        return ResultadoCheckPrendaLeida.USUARIO_FIJADO_NO_CORRESPONDE;
      }

      if (PrendaLeidaHoyMismoPuntoLectura(Prenda, TipoPuntoLectura))
      {
        return ResultadoCheckPrendaLeida.PRENDA_YA_LEIDA_EN_MISMO_PUNTO;
      }

      if (Prenda.FechaBaja != null && Prenda.FechaBaja < DateTime.Now)
      {
        return ResultadoCheckPrendaLeida.PRENDA_SUPERADA_FECHA_BAJA;
      }

      if (Prenda.NLavados > 0 && Prenda.NCiclos >= Prenda.NLavados)
      {
        return ResultadoCheckPrendaLeida.CICLOS_LAVADOS_SUPERADOS;
      }

      return ResultadoCheckPrendaLeida.OK;
    }



    public static bool PrendaLeidaHoyMismoPuntoLectura(GarmentRFModel prenda, int puntoLectura)
    {
      return prenda.UltimoMovimiento == puntoLectura
           && prenda.FechaUltimoMovimiento?.Date == DateTime.Now.Date;

    }

    //public List<string> ObtenerTagsConMovimientoYDiaEspecifico(List<string> listaTags, TipoMovimientoLectura movimiento, DateTime fecha)
    //{
    //  List<string> listado = new List<string>();
    //  int movimientoSeleccionado = movimiento.GetHashCode();
    //  DateTime fechaSeleccionada = fecha.Date;
    //  listado = cnx.MasterPrendas.Where(mp => listaTags.Contains(mp.codigoTAG) &&
    //                                          mp.LastMov == movimientoSeleccionado &&
    //                                          DbFunctions.TruncateTime(mp.DateLastMov) == fechaSeleccionada).Select(s => s.codigoTAG).ToList();
    //  return listado;
    //}
  }
}

