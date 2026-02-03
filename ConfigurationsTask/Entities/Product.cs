namespace ConfigurationsTask.Entities;

public class Product
{
    public  int Id { get; set; }
    public string Name { get; set; }
    public string Desc { get; set; }
    public decimal Price { get; set; }
    public int BrandId { get; set; }
    public Brand Brand { get; set; }
}