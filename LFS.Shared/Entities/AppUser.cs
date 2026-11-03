using Microsoft.AspNetCore.Identity;
using LFS.Shared.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LFS.Shared.Entities
{
    public class AppUser : IdentityUser
    {
        //[PersonalData]
        //[Column(TypeName = "nvarchar(100)")]
        //public string FirstName { get; set; }
        //[PersonalData]
        //[Column(TypeName = "nvarchar(100)")]
        //public string LastName { get; set; }
        public int UsernameChangeLimit { get; set; } = 10;
        public byte[] ProfilePicture { get; set; }
        public int? CustomerId { get; set; }
        public Customer? Customer { get; set; }
        [MaxLength(15)]
        public string Culture { get; set; }


        [PersonalData]
        [Required]
        [MaxLength(100)]
        [Column(TypeName = "nvarchar(100)")]
        public string Name { get; set; }
        [PersonalData]
        [MaxLength(100)]
        [Column(TypeName = "nvarchar(100)")]
        public string SurName1 { get; set; }
        [PersonalData]
        [MaxLength(100)]
        [Column(TypeName = "nvarchar(100)")]
        public string SurName2 { get; set; }
        [PersonalData]
        [MaxLength(10)]
        public string Sex { get; set; }
        [PersonalData]
        public bool Deleted { get; set; }
        [PersonalData]
        public DateTime CreationDate { get; set; }
        [PersonalData]
        public DateTime? FinishDate { get; set; }
        [PersonalData]
        [MaxLength(100)]
        public string RFIDCode { get; set; }
        [PersonalData]
        [MaxLength(500)]
        public string Observations { get; set; }
        [PersonalData]
        public bool Personalized { get; set; }
        [PersonalData]
        public bool NeverDeleted { get; set; }
        [PersonalData]
        public int? IdEmployee { get; set; }
        [PersonalData]
        [MaxLength(50)]
        public string DNI { get; set; }

        public ICollection<AppUserCustomer> UserCustomers { get; set; }
    }
}
