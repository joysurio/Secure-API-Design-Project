using System;

namespace LFS.Shared.Models.Old
{
    public class SubClientHistoryModel
    {
        public string Modelo { get; set; }
        public string Movimiento { get; set; }
        public DateTime? Fecha { get; set; }
        public int StockEnUsoActual { get; set; }
    }
}
