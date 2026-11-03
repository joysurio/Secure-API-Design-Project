using LFS.Shared.Entities;
using System.Collections.Generic;

namespace LFS.Shared.ViewModels
{
    public class DepartmentIndexViewModel
    {
        public int Id { get; set; }
        public string DepartmentDescription { get; set; }
        public int? IdCliente { get; set; }

        public string Cliente { get; set; }
        public string Rango { get; set; }
        public int? LimiteHoras { get; set; } = 0;
        public int? LimitePrendas { get; set; }
        public int RangoCredito { get; set; }
        public int IdCreditDepartment { get; set; }

        public List<MovCreditosModelosDepartamento> Creditos { get; set; }
    }
}
