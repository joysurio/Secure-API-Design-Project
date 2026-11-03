namespace LFS.SecureAPI.Models
{
    public class DeliveryResponse
    {
        public int MovementId { get; set; }
        public int MachineId { get; set; }
        public DateTime ProcessedAt { get; set; }
    }
}
