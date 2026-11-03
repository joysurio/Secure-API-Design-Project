using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LFS.Shared.ViewModels
{
    public class SubCustomerSizeModelViewModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public List<ModeloViewModel> Modelos { get; set; }
    }

    public class ModeloViewModel
    {
        public string Descripcion { get; set; }
        public List<string> Tallas { get; set; }
        public string TallaSeleccionada { get; set; }
    }

}