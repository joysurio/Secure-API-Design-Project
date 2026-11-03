using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LFS.Shared.Entities;

public partial class ImportCategoriaSecundarium
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public int Id { get; set; }
	public string Descripcion { get; set; }

	public int Clave { get; set; }
}
