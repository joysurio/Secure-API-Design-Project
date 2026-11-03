using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LFS.Shared.Entities;

public partial class ImportCategoriaPrincipal
{
	public string Descripcion { get; set; }
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public int Clave { get; set; }
}
