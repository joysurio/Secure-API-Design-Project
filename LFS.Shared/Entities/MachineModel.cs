using System;
using System.ComponentModel.DataAnnotations;

namespace LFS.Shared.Entities
{
    public class MachineModel
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
        public bool Deleted { get; set; }
    }
}
