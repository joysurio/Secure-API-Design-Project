using System;
using System.ComponentModel.DataAnnotations;

namespace LFS.Shared.Entities
{
    public class ImportUserCampoControl
    {
        [Key]
        public int Id { get; set; }
        public string CampoControl { get; set; }
        public bool EstaActivo { get; set; }
    }
}
