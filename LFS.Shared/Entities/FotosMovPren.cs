using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LFS.Shared.Entities;

public partial class FotosMovPren
{
  [Key]
  public int Id { get; set; }

  public byte[] Foto { get; set; }

  public int IdMovPren { get; set; }
}
