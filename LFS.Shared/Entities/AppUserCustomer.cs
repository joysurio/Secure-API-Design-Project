using System;
using System.ComponentModel.DataAnnotations;

namespace LFS.Shared.Entities
{
    public class AppUserCustomer
    {
        [Key]
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }

        [Key]
        public int MasterClientesId { get; set; }
        public MasterClientes MasterClientes { get; set; }
    }
}
