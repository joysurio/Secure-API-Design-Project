using System;
using System.ComponentModel.DataAnnotations;

namespace LFS.Shared.Entities
{
  public partial class iPick
  {
    [Key] public int Id { get; set; }
    public string? id_usuario { get; set; }    
    public string producto { get; set; } 
    public int? cantidad { get; set; } 
    public DateTime? fecha_validacion { get; set; }
    public int? nPreparacion { get; set; }
    public bool? Devolucion { get; set; }
  }
}
