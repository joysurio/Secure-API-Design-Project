namespace LFS.SecureAPI.Models
{
    public class ItemDto
    {
        public int Id { get; set; }
        public string? RFIDCode { get; set; }
        public string? Description { get; set; }
        public int? LocationId { get; set; }
        public int? LastMovementId { get; set; }
        public DateTime? DateLastMov { get; set; }
        public bool Personalised { get; set; }
        public int FamilyItemModelId { get; set; }
        public int? ItemSizeId { get; set; }
    }
}
