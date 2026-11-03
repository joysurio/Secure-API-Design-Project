using System;
using System.Collections.Generic;
using System.Linq;

namespace LFS.Shared.Argument
{
    public class PrendaSuciaSeleccionadaEventArgs : EventArgs
    {
        public PrendaSuciaSeleccionadaEventArgs(Dictionary<int, int?> modelosSeleccionados)
        {
            modelosPrendasSeleccionados = modelosSeleccionados;

            int cantidadModelos = modelosSeleccionados.Where(d => d.Value > 0).Select(d => System.Convert.ToInt32(d.Value)).ToList().Count();

            if (cantidadModelos == 1)
                listaModelosPrendasSeleccionados = new int[] { modelosSeleccionados.Where(d => d.Value > 0).Select(d => System.Convert.ToInt32(d.Value)).FirstOrDefault() };
            else if (cantidadModelos == 2)
                listaModelosPrendasSeleccionados = new int[] { modelosSeleccionados[0].Value, modelosSeleccionados[1].Value };
        }

        public Dictionary<int, int?> modelosPrendasSeleccionados;
        public int[] listaModelosPrendasSeleccionados;
    }
}
