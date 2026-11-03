using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LFS.Shared.ViewModels
{
    public class AlertIndexViewModel
    {
        public int Tipo { get; set; }

        public string Aviso { get; set; }

        public string DireccionEmail { get; set; }

        public int? LimiteNumerico { get; set; }

        public bool? LimiteEsMinimo { get; set; }

        public bool Habilitado { get; set; }

        public string NumerosSms { get; set; }

    }
}