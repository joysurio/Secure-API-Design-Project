using System;
using System.Collections.Generic;

namespace LFS.Shared.Models
{
    public class UserRolesViewModel
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public string SurName1 { get; set; }
        public string SurName2 { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public IEnumerable<string> Roles { get; set; }
    }
}
