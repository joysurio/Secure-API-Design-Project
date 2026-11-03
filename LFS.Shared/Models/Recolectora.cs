using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LFS.Shared.Models
{
  public class Recolectora
  {
    public class CodigoTagDetectado
    {
      public string TagRead { get; set; }
      public DateTime DateDetection { get; set; }
      public int IdPre { get; set; }
      public int UserId { get; set; }
      public string UserName { get; set; } = "";
      public int ModelId { get; set; }
      public string Description { get; set; } = "";
      public string Size { get; set; } = string.Empty;
      public DateTime? DateLastReading { get; set; }
      public bool Personalized { get; set; }
      public int CheckingReadings { get; set; }
      public int CheckModel { get; set; }
      public int UltimoMovimiento { get; set; }

      public int IdUltimoMovimiento { get; set; }
    }
  }
}
