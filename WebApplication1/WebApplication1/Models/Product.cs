namespace WebApplication1.Models;

public class Product
{
    public int Id {get; set; }
    public string Name { get; set; } = "";
    public string Description { get; set; } = "";
    public decimal Price {get; set; }
    public string ImageUrl { get; set; } = "";
    public DateTime CreatedOn {get; set; } =  DateTime.Now;
    public DateTime ModifiedOn {get; set; } = DateTime.Now;
    public int CategoryId {get; set; }
    public Category Category { get; set; } = null!;
    public int BrandId {get; set; }
    public Brand Brand { get; set; } = null!;
    public int Quantity { get; set; }
    public List<Order> Orders { get; set; } = new List<Order>();


}