namespace DBWebApp.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DBWebApp.ViewModels;
using DBWebApp.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using DBWebApp.Models;

[Route("dbapi/[controller]")]
[ApiController]
public class SearchController : Controller
{
    private readonly ApplicationDbContext _context;

    public SearchController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpPost("SearchDB")]
    public async Task<IActionResult> SearchDB(SearchViewModel searchModel)
    {
        var query = _context.CustomerOrders
            .Include(co => co.OrderStatus)  // First JOIN
            .Include(co => co.ShippingMethod)  // Second JOIN
            .AsQueryable();
        
        // Date to UTC
        searchModel.StartDate = searchModel.StartDate?.ToUniversalTime();
        searchModel.EndDate = searchModel.EndDate?.ToUniversalTime();
        
        // Search by order number
        if (searchModel.StartDate.HasValue)
            query = query.Where(co => co.OrderPlacedDatetime >= searchModel.StartDate.Value);

        if (searchModel.EndDate.HasValue)
            query = query.Where(co => co.OrderPlacedDatetime <= searchModel.EndDate.Value);

        // Search by order number
        if (searchModel.Statuses != null && searchModel.Statuses.Any())
        {
            query = query.Where(co => searchModel.Statuses.Contains(co.OrderStatus.OrderStatusCode));
        }

        // Search by order number
        if (!string.IsNullOrEmpty(searchModel.ShippingMethodStart))
        {
            query = query.Where(co => co.ShippingMethod.ShippingMethodDesc.StartsWith(searchModel.ShippingMethodStart));
        }

        // Search by order number
        if (!string.IsNullOrEmpty(searchModel.ShippingMethodEnd))
        {
            query = query.Where(co => co.ShippingMethod.ShippingMethodDesc.EndsWith(searchModel.ShippingMethodEnd));
        }

        var result = await query.ToListAsync();  // Asynchronous query execution

        // DateTime to Ukrainian time zone
        var ukraineTimeZone = TimeZoneInfo.FindSystemTimeZoneById("FLE Standard Time");
        foreach (var order in result)
        {
            order.OrderPlacedDatetime = TimeZoneInfo.ConvertTimeFromUtc(order.OrderPlacedDatetime, ukraineTimeZone);
        }

        return Json(result);  // Return JSON
    }
}