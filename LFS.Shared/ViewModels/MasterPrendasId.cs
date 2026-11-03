using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LFS.Shared.ViewModels
{
  public class MasterPrendasId
  {
    public int IdPrenda { get; set; }
    public int Id_TipoPrenda { get; set; }
    public string[] Data { get; set; }
  }
}
