using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LFS.Shared.Entities
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(150)]
        public string BusinessName { get; set; }
        public DateTime CreationDate { get; set; }
        public bool Deleted { get; set; }
    }
}
