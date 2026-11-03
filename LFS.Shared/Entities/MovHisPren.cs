using System;
using System.Collections.Generic;

namespace LFS.Shared.Entities;

public partial class MovHisPren
{
    public int? IdPre { get; set; }

    public int? IdSubCliOld { get; set; }

    public int? IdSubCliNew { get; set; }

    public DateTime? FechaMov { get; set; }

    public int? Usuario { get; set; }

    public int Id { get; set; }
}
