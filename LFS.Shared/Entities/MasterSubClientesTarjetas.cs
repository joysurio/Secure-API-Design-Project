using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LFS.Shared.Entities
{
  public class MasterSubClienteTarjeta
  {
    [Key]
    public int Id { get; set; }

    [Required]    
    public int IdMasterSubCliente { get; set; }

    [Required]
    [MaxLength(50)]
    public string CodigoTarjeta { get; set; }

    [Required]
    [DefaultValue(false)]
    public bool Borrado { get; set; }

    [ForeignKey("IdMasterSubCliente")]
    public virtual MasterSubClientes MasterSubClientes { get; set; }
  }
}
