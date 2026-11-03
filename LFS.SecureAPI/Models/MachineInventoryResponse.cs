namespace LFS.SecureAPI.Models
{
    public class MachineInventoryResponse
    {
        public int MachineId { get; set; }
        public List<AlmacenDto> Almacenes { get; set; } = new();
        public List<DepartamentoDto> Departamentos { get; set; } = new();
    }

    public class AlmacenDto
    {
        public int Id { get; set; }
        public int Stock { get; set; }
        public int MaximumQuantity { get; set; }
    }

    public class DepartamentoDto
    {
        public int IdDepartamento { get; set; }
    }
}
