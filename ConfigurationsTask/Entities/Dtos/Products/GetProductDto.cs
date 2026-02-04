namespace ConfigurationsTask.Entities.Dtos.Products;

public class GetProductDto
{
    public string Name { get; set; }
    public string Desc { get; set; }
    public decimal Price { get; set; }
    public int BrandId { get; set; }
    public string BrandName { get; set; }
}