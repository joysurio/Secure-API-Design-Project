using System;
using System.ComponentModel.DataAnnotations;

namespace LFS.Shared.Entities.EntitiesOffline
{
  public class MasterPrendas
  {
    public int Id { get; set; }
    public int? Id_ModeloPrenda { get; set; }
    public MasterModeloPrenda MasterModeloPrenda { get; set; }
    public int? Id_Cliente { get; set; }    
    public int? Id_subCliente { get; set; }
    public MasterSubClientes MasterSubClientes { get; set; }
    [MaxLength(50)]
    public string CodigoTAG { get; set; }
    [MaxLength(100)]
    public string Descrip { get; set; }
    public DateTime? FAlta { get; set; }
    public DateTime? FBaja { get; set; }
    public bool? Nueva { get; set; }
    public int? NLavados { get; set; }
    [MaxLength(50)]
    public string Taquilla { get; set; }
    [MaxLength(50)]
    public string Color { get; set; }
    public bool? Accesorio { get; set; }
    [MaxLength(50)]
    public string Talla { get; set; }
    public int? Id_etiqueta { get; set; }
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
    public bool Personalizada { get; set; }
    public int? LastIdMov { get; set; }
    public int? ComprobacionLecturas { get; set; }
    public int? IdUbicacion { get; set; }
    public MovPren MovPren { get; set; }
  }
}
