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
}