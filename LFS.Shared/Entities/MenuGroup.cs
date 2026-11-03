using System.ComponentModel.DataAnnotations;

namespace LFS.Shared.Entities
{
    public class MenuGroup
    {
        public int Id { get; set; }
        [Required, MaxLength(50)]
        public string Description { get; set; }
        public int SortOrder { get; set; }
        public string ResourceString { get; set; }
        public string ClassName { get; set; }
        public bool Active { get; set; }
        public int? ParentId { get; set; }
        public MenuGroup Parent { get; set; }
    }
}
