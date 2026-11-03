using LFS.Shared.Enums.Old;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace LFS.Shared.Entities;

public partial class MasterSubClientes
{
  public int Id { get; set; }

  public int? IdCliente { get; set; }

  public string Nombre { get; set; }

  public string Apellido1 { get; set; }

  public short? Sexo { get; set; }

  public string Puesto { get; set; }

  public string Turno { get; set; }

  public string Vestuario { get; set; }

  public string Taquilla { get; set; }

  public int? IdDepartamentoCliente { get; set; }

  public int? IdEtiqueta { get; set; }

  public bool? Borrado { get; set; }

  public byte[] Foto { get; set; }

  public string Obser { get; set; }

  public string Campo1 { get; set; }

  public string Campo2 { get; set; }

  public string Apellido2 { get; set; }

  public DateTime? Falta { get; set; }

  public DateTime? Fbaja { get; set; }

  public string CodigoTag { get; set; }

  public byte[] UpsizeTs { get; set; }

  public int? LocSopl { get; set; }

  public bool? RopaPersonalizada { get; set; }

  public string Telefono { get; set; }

  public bool DesactivacionManual { get; set; }

  public bool Generico { get; set; }

  public bool? EnUso { get; set; }

  public DateTime? Fnacimiento { get; set; }

  public string Dni { get; set; }

  public string IdEmpleado { get; set; }

  public string Categoria { get; set; }

  public string CodigoQr { get; set; }

  public string Colectivo { get; set; }

  public int PrendasEnUso { get; set; }

  public virtual MasterClientes IdClienteNavigation { get; set; }

  public virtual MasterDepartamentosCliente IdDepartamentoClienteNavigation { get; set; }

  public virtual ICollection<MasterPrendas> MasterPrenda { get; set; } = new List<MasterPrendas>();

  public virtual ICollection<MovHistoricosFechasSubCliente> MovHistoricosFechasSubClientes { get; set; } = new List<MovHistoricosFechasSubCliente>();

  public virtual ICollection<MovModSubCliente> MovModSubClientes { get; set; } = new List<MovModSubCliente>();

  public virtual ICollection<MovModeloTallaSub> MovModeloTallaSubs { get; set; } = new List<MovModeloTallaSub>();

  public virtual ICollection<MovStockPrendasUsu> MovStockPrendasUsus { get; set; } = new List<MovStockPrendasUsu>();

  public virtual ICollection<MovSubClienteIdentificacionManual> MovSubClienteIdentificacionManuals { get; set; } = new List<MovSubClienteIdentificacionManual>();

  [NotMapped]
  public virtual EnumTipoUsuario TipoUsuario { get; set; }
}
