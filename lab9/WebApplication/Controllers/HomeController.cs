namespace WebApplication.Controllers;

using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Models;

[Route("[controller]")]
[ApiController]
public class HomeController : Controller
{
    [HttpGet]
    [ApiVersion("1.0")]
    public IActionResult Index()
    {
        return View();
    }
    
    [HttpGet]
    [ApiVersion("2.0")]
    public IActionResult IndexV2()
    {
        return View();
    }
}