using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LFS.Shared.ViewModels
{
  public class ListOfItemUserCreditAndCardViewModel
  {
    public List<ItemUserCreditViewModel> CreditosUsuarios { get; set; }
    public List<string> Tarjetas { get; set; }
  }
}
