using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LFS.Shared.Entities
{
    public class Department
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(300)]
        public string Description { get; set; }
        public bool Deleted { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime? LastUpdatedOn { get; set; }
        public int LastUpdatedBy { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public ICollection<CreditModelDepartment> CreditsModelDepartment { get; set; }
    }
}
