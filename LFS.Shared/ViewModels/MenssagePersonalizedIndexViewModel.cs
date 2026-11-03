using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LFS.Shared.ViewModels
{
    public class MenssagePersonalizedIndexViewModel
  {
        public int Id { get; set; }
        public int TipoMensaje { get; set; }
        public int IdMaquina { get; set; }
        public string Mensaje { get; set; }

    }
}