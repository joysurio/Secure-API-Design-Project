using System;
using System.ComponentModel.DataAnnotations;

namespace LFS.Shared.Entities
{
    public class LoadUser
    {
        public int Id { get; set; }
        [Required, MaxLength(50)]
        public string Name { get; set; }
        [Required, MaxLength(50)]
        public string TagCode { get; set; }
        public bool Active { get; set; }
        public DateTime CreationDate { get; set; }
        public bool Deleted { get; set; }
        public int? MachineId { get; set; }
        public Machine Machine { get; set; }
        [MaxLength(15)]
        public string Culture { get; set; }
    }
}
