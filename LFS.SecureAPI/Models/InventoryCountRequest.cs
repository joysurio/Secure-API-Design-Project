using System.ComponentModel.DataAnnotations;

namespace LFS.SecureAPI.Models
{
    public class InventoryCountRequest
    {
        [Required]
        public Guid OperationId { get; set; }

        [Required]
        public string DeviceId { get; set; } = string.Empty;

        public int? UserId { get; set; }

        [Required]
        public int LocationId { get; set; }

        public List<InventoryCountItem> Counts { get; set; } = new();
    }

    public class InventoryCountItem
    {
        [Required]
        public string RFIDCode { get; set; } = string.Empty;

        public int Quantity { get; set; } = 1;
    }
}
