using System;
using System.Collections.Generic;

namespace LFS.Shared.Entities;

public partial class FotosPrenda
{
    public int Id { get; set; }

    public byte[] Foto { get; set; }

    public int? IdPrenda { get; set; }
}
