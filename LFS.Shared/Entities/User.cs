using System;
using System.ComponentModel.DataAnnotations;

namespace LFS.Shared.Entities
{
  public class User
  {
    [Key]
    public int Id { get; set; }
    [Required]
    [MaxLength(100)]
    public string Name { get; set; }
    [MaxLength(100)]
    public string SurName1 { get; set; }
    [MaxLength(100)]
    public string SurName2 { get; set; }
    [MaxLength(10)]
    public string Sex { get; set; }
    public bool Deleted { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime? FinishDate { get; set; }
    [MaxLength(100)]
    public string RFIDCode { get; set; }
    [MaxLength(500)]
    public string Observations { get; set; }
    public bool Personalized { get; set; }
    public bool NeverDeleted { get; set; }
    public int? IdEmployee { get; set; }
    [MaxLength(50)]
    public string DNI { get; set; }
  }
}
