using System;
using System.Collections.Generic;

namespace LFS.Shared.Entities;

public partial class FingerTable
{
    public int Id { get; set; }

    public byte[] Blob { get; set; }
}
