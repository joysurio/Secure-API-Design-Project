using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LFS.Shared.Models
{
  public class FotosMovPrenModel
  {
    public int Id { get; set; }

    public byte[] Foto { get; set; }

    public int IdMovPren { get; set; }
  }
}
