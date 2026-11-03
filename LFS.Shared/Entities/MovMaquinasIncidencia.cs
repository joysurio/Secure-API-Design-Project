using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LFS.Shared.Entities;

public partial class MovMaquinasIncidencia
{
    //[Key]
    //public int Id { get; set; }
    public DateTime Fecha { get; set; }

    public int IdMaquina { get; set; }

    public int? IdIncidencia { get; set; }

    public int? IdAlmacen { get; set; }

    [MaxLength(200)]
    public string Descripcion { get; set; }
}
