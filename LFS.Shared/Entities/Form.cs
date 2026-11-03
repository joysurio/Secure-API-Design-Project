using System.ComponentModel.DataAnnotations;

namespace LFS.Shared.Entities
{
    public class Form
    {
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(100)]
        public string Name { get; set; }
        public string ClassString { get; set; }
        public string StringMatch { get; set; }
        public string Url { get; set; }
        public int? MenuGroupId { get; set; }
        public MenuGroup MenuGroup { get; set; }
        public string ResourceString { get; set; }
        public int SortOrder { get; set; }
        public bool Create { get; set; } = false;
        public bool Edit { get; set; } = false;
        public bool Delete { get; set; } = false;
        public bool Active { get; set; } = true;
        public bool OnlyAdmin { get; set; } = false;
        public bool AuthenticationRequired { get; set; } = true;
    }
}
