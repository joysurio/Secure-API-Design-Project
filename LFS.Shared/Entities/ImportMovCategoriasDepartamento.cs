using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LFS.Shared.Entities;

public partial class ImportMovCategoriasDepartamento
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public int Id { get; set; }
	public int ClavePrincipal { get; set; }

    public int? ClaveSecundaria { get; set; }

    public int IdDepartamento { get; set; }
}
