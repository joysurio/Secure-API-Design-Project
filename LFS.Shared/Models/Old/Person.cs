using LFS.Shared.Enums.Old;
using System;

namespace LFS.Shared.Models.Old
{
  public class Person
  {
    public Person()
    {
    }

    public Person(EnumTipoUsuario tipoUsuario)
    {
      this.tipoUsuario = tipoUsuario;
    }

    public Person(int id, string nombre, string apellido1, string apellido2, bool enUso, DateTime? fechaAlta, DateTime? fechaBaja, string campo1, string campo2, string codigoTag, EnumTipoUsuario tipoUsuario, int? idDepartamento, bool prendasPersonalizadas)
    {
      Id = id;
      Nombre = nombre;
      Apellido1 = apellido1;
      Apellido2 = apellido2;
      EnUso = enUso;
      FechaAlta = fechaAlta;
      FechaBaja = fechaBaja;
      Campo1 = campo1;
      Campo2 = campo2;
      CodigoTag = codigoTag;
      this.tipoUsuario = tipoUsuario;
      IdDepartamento = idDepartamento;
      PrendasPersonalizadas = prendasPersonalizadas;
    }

    public int Id { get; set; }
    public string Nombre { get; set; }
    public string Apellido1 { get; set; }
    public string Apellido2 { get; set; }
    public bool EnUso { get; set; } = false;
    public DateTime? FechaAlta { get; set; }
    public DateTime? FechaBaja { get; set; }
    public string Campo1 { get; set; }
    public string Campo2 { get; set; }
    public string CodigoTag { get; set; }
    public EnumTipoUsuario tipoUsuario { get; set; }
    public int? IdDepartamento { get; set; }
    public bool PrendasPersonalizadas { get; set; }
    public int idCliente { get; set; }
    public string Categoria { get; set; }
    public string CodigoQR { get; set; }
    public string DNI { get; set; }
    public DateTime? FechaNacimiento { get; set; }
    public string IdEmpleado { get; set; }
    public string Puesto { get; set; }
    public string Telefono { get; set; }
    public string Taquilla { get; set; }
    public string Vestuario { get; set; }
    public int? IdDotacion { get; set; }
    public int Color { get; set; }
    public bool EsUserFord { get; set; }
    public int PrendasEnUso { get; set; }

    public string NombreCompleto
    {
      get
      {
        string[] valores = new[] { Nombre, Apellido1, Apellido2 };
        var nCompleto = string.Join(" ", valores).Trim();
        return nCompleto;
      }
    }

    public bool EstaActivo
    {
      get
      {
        return EstaEntreRangoFechas();
      }
    }

    private bool EstaEntreRangoFechas()
    {
      DateTime dFechaBaja;
      bool ok = false;
      if (FechaBaja == null)
        ok = true;
      else
      {
        dFechaBaja = (DateTime)FechaBaja;
        if (FechaBaja.HasValue)
          dFechaBaja = ((DateTime)FechaBaja).AddDays(1);
        ok = (FechaAlta.HasValue && FechaAlta <= DateTime.Now) && (FechaBaja.HasValue && dFechaBaja >= DateTime.Now);
      }
      return ok;
    }
  }
}
