using Microsoft.AspNetCore.Mvc;
using System;

namespace LFS.Shared.Models
{
    public class RoleDetailDto
    {
        public int Id { get; set; }
        public string IdentityRoleId { get; set; }
        public string IdentityRoleName { get; set; }
        public int FormId { get; set; }
        public string FormName { get; set; }
        [BindProperty]
        public bool Create { get; set; }
        [BindProperty]
        public bool Edit { get; set; }
        [BindProperty]
        public bool Index { get; set; }
        [BindProperty]
		public bool Delete { get; set; }
        public bool CanCreate { get; set; }
        public bool CanEdit { get; set; }
        public bool CanDelete { get; set; }
        public DateTime CreationDate { get; set; }
        public int? MenuId { get; set; }
        public string MenuName { get; set; }
    }
}
