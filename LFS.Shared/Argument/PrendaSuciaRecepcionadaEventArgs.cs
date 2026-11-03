using System;
using System.Collections.Generic;

namespace LFS.Shared.Argument
{
    public class PrendaSuciaRecepcionadaEventArgs : EventArgs
    {
        public PrendaSuciaRecepcionadaEventArgs(Dictionary<int, bool> prendasRecepcionadas)
        {
            PrendaSuciaRecepcionada = prendasRecepcionadas;
        }

        public Dictionary<int, bool> PrendaSuciaRecepcionada;
    }
}
