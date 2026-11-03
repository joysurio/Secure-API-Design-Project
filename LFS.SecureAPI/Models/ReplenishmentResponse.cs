namespace LFS.SecureAPI.Models
{
    public class ReplenishmentResponse
    {
        public Guid IdRep { get; set; }
        public int MachineId { get; set; }
        public DateTime ProcessedAt { get; set; }
    }
}
