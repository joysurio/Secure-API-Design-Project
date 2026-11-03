using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;


namespace LFS.Shared.ViewModels
{
  public class MachineModelStoreViewmodel
  {
    [BindProperty]
    public int IdMaquina { get; set; }
    [BindProperty]
    public int? IdModeloPrenda { get; set; }
    [BindProperty]
    public int? IdStore { get; set; }
    public IEnumerable<SelectListItem> Machines { get; set; }
    public IEnumerable<SelectListItem> ModelosPrenda { get; set; }
    public IEnumerable<SelectListItem> Stores { get; set; }
  }
}
