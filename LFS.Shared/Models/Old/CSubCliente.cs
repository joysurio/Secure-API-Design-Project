using LFS.Shared.Entities;
using LFS.Shared.Enums.Old;
using LFS.Shared.Models.Old;

namespace LFS.Shared.Models.Old
{
  public class CSubCliente : Person
  {
    public CSubCliente() : base(EnumTipoUsuario.Cliente)
    {
    }

    public short? Sexo { get; set; }
    public string Idioma { get; set; }
    public MasterClientes Cliente { get; set; }
  }
}

