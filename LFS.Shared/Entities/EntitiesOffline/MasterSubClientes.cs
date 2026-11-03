using System;
using System.ComponentModel.DataAnnotations;

namespace LFS.Shared.Entities.EntitiesOffline
{
  public class MasterSubClientes
  {
    public int Id { get; set; }
    public int? Id_Cliente { get; set; }
    [MaxLength(100)]
    public string? Nombre { get; set; }
    [MaxLength(150)]
    public string? Apellido1 { get; set; }
    //public short? Sexo { get; set; }
    //[MaxLength(200)]
    //public string? Puesto { get; set; }
    //[MaxLength(50)]
    //public string? Turno { get; set; }
    //[MaxLength(200)]
    //public string? Vestuario { get; set; }
    [MaxLength(150)]
    public string? Taquilla { get; set; }
    public int? Id_DepartamentoCliente { get; set; }
    //public MasterDepartamentosCliente? MasterDepartamentosCliente { get; set; }
    //public MovCreditosDepartamentos? MovCreditosDepartamentos { get; set; }
    //[MaxLength(50)]
    //public int? Id_Etiqueta { get; set; }
    public bool? Borrado { get; set; }
    //[MaxLength(250)]
    //public string? Obser { get; set; }
    [MaxLength(50)]
    public string? Campo1 { get; set; }
    [MaxLength(50)]
    public string? Campo2 { get; set; }
    [MaxLength(150)]
    public string? Apellido2 { get; set; }
    //public byte[]? Foto { get; set; }
    public DateTime? FAlta { get; set; }
    public DateTime? FBaja { get; set; }
    [MaxLength(50)]
    public string? CodigoTAG { get; set; }
    //public byte[]? Upsize_Ts { get; set; }
    public int? LocSOPL { get; set; }
    public bool? RopaPersonalizada { get; set; }
    [MaxLength(30)]
    public string? Telefono { get; set; }
    public bool DesactivacionManual { get; set; }
    public bool Generico { get; set; }
    public bool? EnUso { get; set; }
    [MaxLength(50)]
    public string CodigoQr { get; set; }
    [MaxLength(50)]
    public string Dni { get; set; }
    [MaxLength(50)]
    public string IdEmpleado { get; set; }
    public int PrendasEnUso { get; set; }
  }
}
