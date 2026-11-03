using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LFS.Shared.ViewModels
{
    public class ItemPositionsViewModel
    {
        public string Descripcion { get; set; }
        public string Talla { get; set; }
        public string TipoMov { get; set; }
        public string Descrip { get; set; }
        public int Count { get; set; }
        public List<UltimoMovimientoPrendaViewModel> Items { get; set; }
    }
}