using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LFS.Shared.Entities.EntitiesOffline
{

    public partial class MasterDepartamentosCliente
    {
        public int Id { get; set; }

        public int? IdCliente { get; set; }

        [MaxLength(50)]
        public string? Descripcion { get; set; }

    }
}
