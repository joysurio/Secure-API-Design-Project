namespace LFS.Shared.Models.Old
{
    public class PrendasDisponiblesSubCliente
    {
        public int Id { get; set; }
        public int IdSubCli { get; set; }
        public int IdMod { get; set; }
        public string Talla { get; set; }
        public string DescModelo { get; set; }
        public bool Asignada { get; set; }
        public int CantidadUso { get; set; }
    }
}
