using System.Reflection;
using System.Drawing;

namespace LFS.Shared.Models.Old
{
    public class PrendasDisponiblesEnMaquinaDispensacion
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public string Talla { get; set; }       
        public decimal StockReal { get; set; }
        public int CantidadUso { get; set; }     
       
    }
}
