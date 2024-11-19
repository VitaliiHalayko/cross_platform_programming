namespace DBWebApp.Controllers;

using Microsoft.AspNetCore.Mvc;
using DBWebApp.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

[Authorize]
[Route("dbapi/[controller]")]
[ApiController]
public class ProductController : Controller
{
    private readonly ApplicationDbContext _context;

    public ProductController(ApplicationDbContext context)
    {
        _context = context;
    }

    // Action to list all products, returning data as JSON
    [HttpGet("Product")]
    public async Task<IActionResult> Index()
    {
        var products = await _context.Products.ToListAsync(); // Await completion of the query
        return Json(products);  // Return products as JSON
    }

    // Action to view a single product by ID, returning data as JSON
    [HttpGet("Product/{id}")]
    public async Task<IActionResult> DetailsDB(int id)
    {
        var product = await _context.Products
            .FirstOrDefaultAsync(p => p.ProductId == id);  // Await completion of the query
        return Json(product);  // Return product as JSON
    }
}