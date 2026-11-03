using System;
using System.Collections.Generic;

namespace LFS.Shared.Entities;

public partial class MasterUsuario
{
    public int Id { get; set; }

    public int? IdTipoUsuario { get; set; }

    public string Nombre { get; set; }

    public string Apellido1 { get; set; }

    public string Apellido2 { get; set; }

    public bool? Pedirporpantalla { get; set; }

    /// <summary>
    /// codigo de lector o de contraseÃ±a por si quiere no llevar chip
    /// </summary>
    public string Usuario { get; set; }

    public string Pwd { get; set; }

    public bool? Activo { get; set; }

    public string CodigoTag { get; set; }

    public Guid? LavanderUserId { get; set; }

    public virtual ICollection<MovCargasDescargasMaquina> MovCargasDescargasMaquinas { get; set; } = new List<MovCargasDescargasMaquina>();
}
