using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace LFS.Shared.Entities
{
    public class ItemUser
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }
        [MaxLength(50)]
        public string LastName2 { get; set; }
        [MaxLength(50)]
        public string EmployeeId { get; set; }
        [MaxLength(50)]
        public string RFId { get; set; }
        public int SexId { get; set; }
        public Sex Sex { get; set; }
        [MaxLength(50)]
        public string Position { get; set; }
        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }
        [MaxLength(15)]
        public string Telephone { get; set; }
        public DateTime DateOfRegistration { get; set; }
        [DataType(DataType.Date)]
        public DateTime? DateOfDeRegistration { get; set; }
        [MaxLength(50)]
        public string AuxField1 { get; set; }
        [MaxLength(50)]
        public string AuxField2 { get; set; }
        public int DepartmentId { get; set; }
        public Department Department { get; set; }
        public bool Deleted { get; set; }
        public DateTime CreationDate { get; set; }
        [MaxLength(15)]
        public string Culture { get; set; }
        public ICollection<CreditItemUser > CreditsItemUser { get; set; }
        public string Name
        {
            get
            {
                return string.Format("{0} {1}", FirstName, LastName);
            }
        }
    }
}
