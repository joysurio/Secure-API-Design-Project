using Microsoft.AspNetCore.Identity;
using System;

namespace LFS.Shared.Entities
{
    public class RoleDetail
    {
        public int Id { get; set; }
        public string IdentityRoleId { get; set; }
        public IdentityRole IdentityRole { get; set; }
        public int FormId { get; set; }
        public Form Form { get; set; }
        public bool Create { get; set; }
        public bool Edit { get; set; }
        public bool Delete { get; set; }
        public bool Index { get; set; }
		public DateTime CreationDate { get; set; }
    }
}
