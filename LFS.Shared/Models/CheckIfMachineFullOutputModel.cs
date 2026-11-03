using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LFS.Shared.Models
{
  public class CheckIfMachineFullOutputModel
  {
    public bool MaquinaEstaLlena { get; set; }
    public bool EnviarEmailMaquinaLlena { get; set; }

    public int NumeroPrendas { get; set; }

  }
}
