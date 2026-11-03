namespace LFS.SecureAPI.Models
{
    public class InsertMovPrenResponse
    {
        public int Id { get; set; }
        public int IdPrenda { get; set; }
        public bool PrendaCreated { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
