using System;
using System.ComponentModel.DataAnnotations;

namespace LFS.Shared.Entities
{
  public partial class iPickCarros
  {
    [Key] public int id { get; set; }
    public string Descripcion { get; set; }    
    public bool? EnUso { get; set; } 
  }
}
