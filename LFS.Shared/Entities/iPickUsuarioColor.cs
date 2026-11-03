using System;
using System.ComponentModel.DataAnnotations;

namespace LFS.Shared.Entities
{
  public partial class iPickUsuarioColor
  {
    [Key] public int Id { get; set; }
    public string? id_usuario { get; set; }    
    public string nombre { get; set; } 
    public int? color { get; set; } 
  }
}
