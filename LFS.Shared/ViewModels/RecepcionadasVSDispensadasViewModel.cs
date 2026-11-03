using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LFS.Shared.ViewModels
{
    public class RecepcionadasVSDispensadasViewModel
    {
      public DateTime? Fecha { get; set; }
      public string Nombre { get; set; }
      public int Dispensadas { get; set; }
      public int Recepcionadas { get; set; }
      public int Diferencia { get; set; }
    }
}
