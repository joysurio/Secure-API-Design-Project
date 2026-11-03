namespace LFS.SecureAPI.Models
{
    public class MachineLoadResponse
    {
        public int Id { get; set; }
        public int MachineId { get; set; }
        public int TipoMov { get; set; }
        public DateTime ProcessedAt { get; set; }
    }
}
