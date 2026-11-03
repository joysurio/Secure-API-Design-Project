namespace LFS.SecureAPI.Models
{
    public class InventoryReconcileResponse
    {
        public int LocationId { get; set; }
        public int ItemCount { get; set; }
        public List<ItemDto> Items { get; set; } = new();
    }
}
