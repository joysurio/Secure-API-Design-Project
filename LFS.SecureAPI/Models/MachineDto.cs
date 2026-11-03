namespace LFS.SecureAPI.Models
{
    public class MachineDto
    {
        public int Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public int? IdCliente { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
