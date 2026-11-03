
using LFS.Shared.Entities;
using System;

namespace LFS.Shared.Entities
{
  public class MovMaquinaTiempoDispensado
  {
    public int Id { get; set; }
    public int IdMaquina { get; set; }
    public int IdCliente { get; set; }
    public int IdModelo { get; set; }
    public DateTime FechaHora { get; set; }
    public decimal TDispensadoEnSegundos { get; set; }
  }
}
