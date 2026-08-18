using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Controllers;

public class CategoryController : Controller
{
    private readonly ShopContext _context;
    public CategoryController(ShopContext context)
    {
        _context = context;
    }
    public IActionResult Index()
    {
        List<Category> categories = _context.Categories.Include(c => c.Products).ToList();
        return View(categories);
    }
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(Category category)
    {
        if (category != null)
        {
            string name = category.Name.Trim();

            bool exists = _context.Categories.Any(c => c.Name.ToLower() == name.ToLower());

            if (exists)
            {
                ViewBag.Message = "Category with this name already exists";
                return View(category);
            }

            category.Name = name;
            category.Description = category.Description?.Trim() ?? "";

            _context.Categories.Add(category);
            _context.SaveChanges();
        }

        return RedirectToAction("Index");
    }
    
    public IActionResult Delete(int id)
    {
        Category? category = _context.Categories.FirstOrDefault(c => c.Id == id);

        if (category == null)
        {
            TempData["Message"] = "Category was not found";
            return RedirectToAction("Index");
        }

        bool hasProducts = _context.Products.Any(p => p.CategoryId == id);

        if (hasProducts)
        {
            TempData["Message"] = "This category cannot be deleted because it has products.";
            return RedirectToAction("Index");
        }

        _context.Categories.Remove(category);
        _context.SaveChanges();

        return RedirectToAction("Index");
    }
}