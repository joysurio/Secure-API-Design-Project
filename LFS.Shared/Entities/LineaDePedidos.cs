using System;
using System.ComponentModel.DataAnnotations;

namespace LFS.Shared.Entities
{
  public partial class LineaDePedidos
  {
    [Key] public int Id { get; set; }
    public int? numeroPedido { get; set; }
    public string numeroPaciente { get; set; }
    public string nombrePaciente { get; set; }
    public string cama { get; set; }
    public string unidad { get; set; }
    public int codigoProducto { get; set; }
    public string producto { get; set; }
    public int? cantidad { get; set; }
    public bool? procesada { get; set; }
    public DateTime? fechaProceso { get; set; }
    public string usuario { get; set; }
    public int? nPreparacion { get; set; }
    public string Descripcion { get; set; }
    public int? numeroCarro { get; set; }
  }
}
