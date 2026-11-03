using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LFS.Shared.Entities
{
    public class MovStockTaquilasTemps
    {
        public int Id { get; set; }
        [MaxLength(50)]
        public string CodTag { get; set; }
        public int? Taquila { get; set; }
        public DateTime? LastRead { get; set; }
    }
}
