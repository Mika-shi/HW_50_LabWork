using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Controllers;

public class ProductController : Controller
{
    private readonly ShopContext _context;
    
    public  ProductController(ShopContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        List<Product> products = _context.Products.Include(p => p.Category).Include(p => p.Brand).ToList();
        return View(products);
    }

    public IActionResult Create()
    {
        ViewBag.Categories = new SelectList(_context.Categories.ToList(), "Id", "Name");
        ViewBag.Brands = new SelectList(_context.Brands.ToList(), "Id", "Name");
        return View();
    }

    [HttpPost]

    public IActionResult Create(Product product)
    {
        bool categoryExists = _context.Categories.Any(c => c.Id == product.CategoryId);
        bool brandExists = _context.Brands.Any(b => b.Id == product.BrandId);

        if (!categoryExists || !brandExists)
        {
            ViewBag.Message = "Please choose existing category and brand";
            ViewBag.Categories = new SelectList(_context.Categories.ToList(), "Id", "Name");
            ViewBag.Brands = new SelectList(_context.Brands.ToList(), "Id", "Name");
            
            return View(product);
        }
        
        product.Name = product.Name?.Trim() ?? "";
        product.Description = product.Description?.Trim() ?? "";
        product.ImageUrl = product.ImageUrl?.Trim() ?? "";
        product.CreatedOn = DateTime.Now;
        product.ModifiedOn = DateTime.Now;
        
        _context.Products.Add(product);
        _context.SaveChanges();
        
        return RedirectToAction("Index");
    }
    
    public IActionResult Details(int id)
    {
        Product? product = _context.Products.Include(p => p.Category).Include(p => p.Brand).FirstOrDefault(p => p.Id == id);

        if (product == null)
        {
            return NotFound();
        }
        return View(product);
    }
    
}