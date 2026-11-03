using System.ComponentModel.DataAnnotations;

namespace LFS.Shared.Entities
{
    public class Alert
    {
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(100)]
        public string Description { get; set; }
        public string Addresses { get; set; }
        public bool Enabled { get; set; } = true;
        public int? Value { get; set; }
    }
}
