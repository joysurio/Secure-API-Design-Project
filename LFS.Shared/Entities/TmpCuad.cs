using System;
using System.Collections.Generic;

namespace LFS.Shared.Entities;

public partial class TmpCuad
{
    public double? Serie { get; set; }

    public DateTime? Fecha { get; set; }

    public string Puesto { get; set; }

    public double? IdCli { get; set; }

    public double? IdsubCli { get; set; }

    public double? IdPre { get; set; }

    public bool? TipoOrd { get; set; }

    public int? NcolTot { get; set; }

    public int? NrowTot { get; set; }

    public string Col { get; set; }

    public string Row { get; set; }

    public int? Nmov { get; set; }

    public bool? FinalProc { get; set; }

    public int Id { get; set; }
}
