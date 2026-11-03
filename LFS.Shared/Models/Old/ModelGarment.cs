namespace LFS.Shared.Models.Old
{
  public class ModelGarment : GarmentModel
  {
    public string Talla { get; set; }
    public int StockReal { get; set; }
    public int CantidadUso { get; set; }
    public int Credito { get; set; }
    public int Total { get; set; }
    public int EnUbicacion { get; set; }
    public int EnUso { get; set; }
    public string ModelosEnParejaPrendas { get; set; } = "";
    public ModelGarment PrendaPrincipal { get; set; }
    public string CodigoPrendaFord { get; set; }   
    public decimal Precio { get; set; }
  }
}
