using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LFS.Shared.Entities;

public partial class MovSubClienteIdioma
{
	[Key]
	public int IdSubCliente { get; set; }

	public string Idioma { get; set; }

	public virtual MasterSubClientes IdSubClienteNavigation { get; set; }
}
