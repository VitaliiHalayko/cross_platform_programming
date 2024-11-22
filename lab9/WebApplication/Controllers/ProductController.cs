namespace WebApplication.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using WebApplication.Models;
using System.Text.Json;
using DBWebApp.Models;

[Authorize]
public class ProductController : Controller
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;
    private readonly string _apiBaseUrl;
    private readonly JsonSerializerOptions _jsonOptions;

    public ProductController(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration;
        
        // Use the HTTP or HTTPS base URL based on the environment
        _apiBaseUrl = _configuration.GetValue<string>("ASPNETCORE_ENVIRONMENT") == "Development" 
            ? _configuration.GetValue<string>("ApiSettings:BaseUrl")
            : _configuration.GetValue<string>("ApiSettings:SecureBaseUrl");

        _jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
    }
    
    public async Task<IActionResult> Data()
    {
        try
        {
            var response = await _httpClient.GetAsync($"{_apiBaseUrl}Product/Product");
            
            if (!response.IsSuccessStatusCode)
            {
                return HandleApiError(response.StatusCode);
            }

            var content = await response.Content.ReadAsStringAsync();
            var products = JsonSerializer.Deserialize<List<Product>>(content, _jsonOptions);

            return View(products ?? new List<Product>());
        }
        catch (HttpRequestException ex)
        {
            // Log the exception
            return View("Error", new ErrorViewModel 
            { 
                Message = $"API Connection Error: {ex.Message}",
                RequestId = HttpContext.TraceIdentifier
            });
        }
    }

    public async Task<IActionResult> Details(int id)
    {
        try
        {
            var response = await _httpClient.GetAsync($"{_apiBaseUrl}Product/Product/{id}");
            
            if (!response.IsSuccessStatusCode)
            {
                return HandleApiError(response.StatusCode);
            }

            var content = await response.Content.ReadAsStringAsync();
            var product = JsonSerializer.Deserialize<Product>(content, _jsonOptions);

            return View(product);
        }
        catch (HttpRequestException ex)
        {
            return View("Error", new ErrorViewModel 
            { 
                Message = $"API Connection Error: {ex.Message}",
                RequestId = HttpContext.TraceIdentifier
            });
        }
    }

    private IActionResult HandleApiError(System.Net.HttpStatusCode statusCode)
    {
        var errorMessage = statusCode switch
        {
            System.Net.HttpStatusCode.NotFound => "Resource not found",
            System.Net.HttpStatusCode.Unauthorized => "Unauthorized access",
            System.Net.HttpStatusCode.BadRequest => "Invalid request",
            _ => "An error occurred while processing your request"
        };

        return View("Error", new ErrorViewModel 
        { 
            Message = errorMessage,
            RequestId = HttpContext.TraceIdentifier
        });
    }
}
