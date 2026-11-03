namespace LFS.SecureAPI.Models
{
    public class MovementResponse
    {
        public int Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public DateTime CreationDate { get; set; }
    }
}
