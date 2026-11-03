using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LFS.Shared.Entities.EntitiesOffline
{

    public partial class MasterUsuarios
    {
        public int Id { get; set; }

        public int? Id_Tipo_Usuario { get; set; }

        [MaxLength(50)]
        public string? Nombre { get; set; }

        [MaxLength(50)]
        public string? Apellido1 { get; set; }

        [MaxLength(50)]
        public string? Apellido2 { get; set; }

        public bool? Pedirporpantalla { get; set; }

        [MaxLength(50)]
        public string? Usuario { get; set; }

        [MaxLength(30)]
        public string? Pwd { get; set; }

        public bool? Activo { get; set; }

        [MaxLength(50)]
        public string? CodigoTag { get; set; }

        public Guid? LavanderUserId { get; set; }

    }
}