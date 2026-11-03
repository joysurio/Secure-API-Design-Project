using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LFS.Shared.ViewModels
{
  public class ItemUserIndexViewModel
  {
    public int Id { get; set; }
    public string NombreYApellidos { get; set; }

    public string Campo1 { get; set; }

    public string Departamento { get; set; }

    public string Cliente { get; set; }

    public int? IdCliente { get; set; }

    public bool? Borrado { get; set; }

    public DateTime? FechaAlta { get; set; }

    public DateTime? FechaBaja { get; set; }

    public short? Sexo { get; set; }

    public string Puesto { get; set; }

    public string Turno { get; set; }

    public string Vestuario { get; set; }

    public string Taquilla { get; set; }

    public int? IdEtiqueta { get; set; }

    public byte[] Foto { get; set; }

    public string Obser { get; set; }

    public string Campo2 { get; set; }

    public string CodigoTag { get; set; }

    public byte[] UpsizeTs { get; set; }

    public int? LocSopl { get; set; }

    public bool RopaPersonalizada { get; set; }

    public string Telefono { get; set; }

    public bool DesactivacionManual { get; set; }

    public bool Generico { get; set; }

    public bool EnUso { get; set; }

    public DateTime? Fnacimiento { get; set; }

    public string Dni { get; set; }

    public string IdEmpleado { get; set; }

    public string Categoria { get; set; }

    public string CodigoQr { get; set; }

    public string Colectivo { get; set; }
    public int PrendasEnUso { get; set; }

  }
}