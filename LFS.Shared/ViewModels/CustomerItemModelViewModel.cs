namespace LFS.Shared.ViewModels
{
    public class CustomerItemModelViewModel
    {
        public int Id { get; set; }
        public int IdModelo { get; set; }
        public string DescripcionModelo { get; set; }
        public int IdCliente { get; set; }
        public string NombreCliente { get; set; }
        public bool selected { get; set; }
    }
}
