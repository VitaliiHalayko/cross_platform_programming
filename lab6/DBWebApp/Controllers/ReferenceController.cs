namespace DBWebApp.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DBWebApp.Data;
using System.Threading.Tasks;
using DBWebApp.Models;
using Microsoft.AspNetCore.Authorization;

[Route("dbapi/[controller]")]
[ApiController]
public class ReferenceController : Controller
{
    private readonly ApplicationDbContext _context;

    public ReferenceController(ApplicationDbContext context)
    {
        _context = context;
    }

    // List of all product categories
    [HttpGet("RefOrderStatusesDB")]
    public async Task<IActionResult> RefOrderStatusesDB()
    {
        var statuses = await _context.RefOrderStatuses.ToListAsync();  // Asynchronous request to get all order statuses
        return Ok(statuses); 
    }

    // Detailed information about each order status
    [HttpGet("RefOrderStatusesDB/{id}")]
    public async Task<IActionResult> RefOrderStatusDetailsDB(string id)
    {
        var details = await _context.RefOrderStatuses
            .FirstOrDefaultAsync(s => s.OrderStatusCode == id);  // Asynchronous request to get order status by ID
        return Ok(details);
    }

    // List of all product categories
    [HttpGet("RefShippingMethodsDB")]
    public async Task<IActionResult> RefShippingMethodsDB()
    {
        var methods = await _context.RefShippingMethods.ToListAsync();  // Asynchronous request to get all shipping methods
        return Ok(methods);
    }

    // Detailed information about each shipping method
    [HttpGet("RefShippingMethodsDB/{id}")]
    public async Task<IActionResult> RefShippingMethodsDetailsDB(string id)
    {
        var details = await _context.RefShippingMethods
            .FirstOrDefaultAsync(s => s.ShippingMethodCode == id);  // Asynchronous request to get shipping method by ID
        return Ok(details);
    }
}
