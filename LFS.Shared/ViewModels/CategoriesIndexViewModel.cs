using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LFS.Shared.ViewModels
{
    public class CategoriesIndexViewModel
    {
        public int Id { get; set; }
        public string CategoriaPrincipal { get; set; }
        public string CategoriaSecundaria { get; set; }
        public string Departamento { get; set; }
    }
}