using System;
using System.ComponentModel.DataAnnotations;

namespace LFS.Shared.Entities
{
    public class Location
    {
        public int Id { get; set; }
        [Required, MaxLength(250)]
        public string Description { get; set; }
        public bool Deleted { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
