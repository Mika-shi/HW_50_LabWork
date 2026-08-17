namespace WebApplication1.Models;

public class Brand
{
    public int Id { get; set; }
    public string Name { get; set; }  = "";
    public string Country { get; set; } = "";
    public List<Product> Products { get; set; } = new List<Product>();
}