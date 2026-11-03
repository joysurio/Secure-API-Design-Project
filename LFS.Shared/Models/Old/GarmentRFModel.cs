using LFS.Shared.Entities;
using System;
using System.Collections.Generic;

namespace LFS.Shared.Models.Old
{
  public class GarmentRFModel : GarmentModel
  {
    public MasterSubClientes Propietario;
    public int idPre { get; set; } = 0;
    public string CodigoTagPre { get; set; }
    public string Talla { get; set; }
    public DateTime? FechaUltimoMovimiento { get; set; }
    public int UltimoMovimiento { get; set; }
    public string UltimoMovimientoDescripcion { get; set; }
    public int UltimoMovimientoPrendaMaquina { get; set; }
    public bool Borrada { get; set; }
    public int idCli { get; set; }
    public int idEtiqueta { get; set; }
    public string Taquilla { get; set; }
    public int NOrden { get; set; }
    public int NTotal { get; set; }
    public bool Personalizada { get; set; }
    public bool Nueva { get; set; }
    public bool Facturable { get; set; }
    public DateTime? FechaAlta { get; set; }
    public DateTime? FechaBaja { get; set; }
    public DateTime? FechaBajaReal { get; set; }
    public int ComprobacionLecturas { get; set; }
    public int ModeloComprobacion { get; set; }
    public byte[] FotoPrenda { get; set; }
    public int NLavados { get; set; }
    public int idUltimoMovimiento { get; set; }
    public int? idTipoRopa { get; set; }
    public int TemperaturaLavadoRopa { get; set; }
    public int TemperaturaSecadoRopa { get; set; }
    public List<int> prendasRecientes;
    public int NCiclos { get; set; }
    public int IdUbicacion { get; set; }
  }

  public class GarmentModel
  {
    public const int ALTOBOTONSELECCIONAMODELO = 110;
    public const int ANCHOBOTONSELECCIONAMODELO = 255;

    public int IdModelo { get; set; }
    public string Descripcion { get; set; }
    public int tipoLocalizacionSOPL { get; set; }
    public int idTipoRopa { get; set; }
    public double TemperaturaLavadoRopa { get; set; }
    public double TemperaturaSecadoRopa { get; set; }
  }
}
