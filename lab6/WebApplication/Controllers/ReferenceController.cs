namespace WebApplication.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using DBWebApp.Models;
using WebApplication.Models;

[Authorize]
public class ReferenceController : Controller
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;
    private readonly string _apiBaseUrl;
    private readonly JsonSerializerOptions _jsonOptions;

    public ReferenceController(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration;
        
        // Use different URLs depending on the environment
        _apiBaseUrl = _configuration.GetValue<string>("ASPNETCORE_ENVIRONMENT") == "Development"
            ? _configuration.GetValue<string>("ApiSettings:BaseUrl")
            : _configuration.GetValue<string>("ApiSettings:SecureBaseUrl");

        _jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
    }
    
    public async Task<IActionResult> RefOrderStatuses()
    {
        try
        {
            var response = await _httpClient.GetAsync($"{_apiBaseUrl}Reference/RefOrderStatusesDB");
            
            if (!response.IsSuccessStatusCode)
            {
                return HandleApiError(response.StatusCode);
            }

            var content = await response.Content.ReadAsStringAsync();
            var refOrderStatuses = JsonSerializer.Deserialize<List<RefOrderStatus>>(content, _jsonOptions);

            return View(refOrderStatuses ?? new List<RefOrderStatus>());
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
    
    public async Task<IActionResult> RefOrderStatusDetails(string id)
    {
        try
        {
            var response = await _httpClient.GetAsync($"{_apiBaseUrl}Reference/RefOrderStatusesDB/{id}");
            
            if (!response.IsSuccessStatusCode)
            {
                return HandleApiError(response.StatusCode);
            }

            var content = await response.Content.ReadAsStringAsync();
            var refOrderStatus = JsonSerializer.Deserialize<RefOrderStatus>(content, _jsonOptions);

            return View(refOrderStatus);
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

    public async Task<IActionResult> RefShippingMethods()
    {
        try
        {
            var response = await _httpClient.GetAsync($"{_apiBaseUrl}Reference/RefShippingMethodsDB");
            
            if (!response.IsSuccessStatusCode)
            {
                return HandleApiError(response.StatusCode);
            }

            var content = await response.Content.ReadAsStringAsync();
            var refShippingMethods = JsonSerializer.Deserialize<List<RefShippingMethod>>(content, _jsonOptions);

            return View(refShippingMethods ?? new List<RefShippingMethod>());
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
    
    public async Task<IActionResult> RefShippingMethodsDetails(string id)
    {
        try
        {
            var response = await _httpClient.GetAsync($"{_apiBaseUrl}Reference/RefShippingMethodsDB/{id}");
            
            if (!response.IsSuccessStatusCode)
            {
                return HandleApiError(response.StatusCode);
            }

            var content = await response.Content.ReadAsStringAsync();
            var refShippingMethod = JsonSerializer.Deserialize<RefShippingMethod>(content, _jsonOptions);

            return View(refShippingMethod);
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
