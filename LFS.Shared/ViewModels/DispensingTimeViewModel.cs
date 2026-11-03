using System;

namespace LFS.Shared.ViewModels
{
  public class DispensingTimeViewModel
  {
    public int Id { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndtDate { get; set; }
    public int Count { get; set; }
    public int IdMaquina { get; set; }
    public int IdCliente { get; set; }
    public int IdModelo { get; set; }
    public DateTime FechaHora { get; set; }
    public decimal TDispensadoEnSegundos { get; set; }
    public string NombreMaquina { get; set; }
    public string NombreCliente { get; set; }
    public string NombreModelo { get; set; }

  }

}
