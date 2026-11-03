using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LFS.Shared.ViewModels
{
    public class GarmentsByIntervalViewModel
    {
        public string Interval { get; set; }
        public int TotalDispensed { get; set; }
        public int UniqueDispensedUsers { get; set; }
        public int TotalCollected { get; set; }
        public int UniqueCollectedUsers { get; set; }
    }
}