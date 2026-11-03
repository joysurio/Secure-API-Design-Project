using System;
using System.Collections.Generic;

namespace LFS.Shared.Entities;

public partial class MovTaquillas
{
    public DateTime? Fecha { get; set; }

    public int? IdMaquina { get; set; }

    public DateTime? HoraLogin { get; set; }

    public int? IdSubCli { get; set; }

    public int? IdModeloPrendaPedido { get; set; }

    public string IdTallaPrendaPedido { get; set; }

    public string CodTagLeido { get; set; }

    public int? IdPrendaLeido { get; set; }

    public int? IdModeloPrendaLeido { get; set; }

    public string IdTallaPrendaLeido { get; set; }

    public int? LastMov { get; set; }

    public DateTime? DateLastMov { get; set; }

    public int? IdMaquinaLastMov { get; set; }
}
