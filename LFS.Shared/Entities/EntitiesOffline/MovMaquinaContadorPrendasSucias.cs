using System;
using System.ComponentModel.DataAnnotations;

namespace LFS.Shared.Entities.EntitiesOffline
{
  public class MovMaquinaContadorPrendasSucias
  {
    public int Id { get; set; }
    public int IdMaquina { get; set; }
    public int IdCliente { get; set; }
    public int Contador { get; set; }
    public DateTime FechaHoraUltimoCambio { get; set; }

  }
}
