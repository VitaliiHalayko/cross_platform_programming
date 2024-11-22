using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Text.Json;
using System.Text;
using System.Threading.Tasks;
using WebApplication.ViewModels;
using WebApplication.Models;
using DBWebApp.Models;

[Authorize]
public class SearchController : Controller
{
    private readonly HttpClient _httpClient;
    private readonly string _apiBaseUrl;
    private readonly JsonSerializerOptions _jsonOptions;

    public SearchController(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _apiBaseUrl = configuration.GetValue<string>("ApiSettings:BaseUrl");
        _jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
    }

    // GET: /Search/ (open search page)
    [HttpGet]
    public IActionResult Search()
    {
        // Return empty list of orders
        return View(new List<CustomerOrder>());
    }

    // POST: /Search/ (get search results)
    [HttpPost]
    public async Task<IActionResult> Search(SearchViewModel searchModel)
    {
        if (ModelState.IsValid)
        {
            try
            {
                // If search model is valid, send it to the API
                var jsonContent = JsonSerializer.Serialize(searchModel, _jsonOptions);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync($"{_apiBaseUrl}Search/SearchDB", content);

                if (!response.IsSuccessStatusCode)
                {
                    return HandleApiError(response.StatusCode);
                }

                var responseContent = await response.Content.ReadAsStringAsync();
                var orders = JsonSerializer.Deserialize<List<CustomerOrder>>(responseContent, _jsonOptions);

                // Return search results
                return View(orders ?? new List<CustomerOrder>());
            }
            catch (HttpRequestException ex)
            {
                return View("Error", new ErrorViewModel
                {
                    Message = $"API Connection Error: {ex.Message}",
                    RequestId = HttpContext.TraceIdentifier
                });
            }
            catch (JsonException ex)
            {
                return View("Error", new ErrorViewModel
                {
                    Message = $"Data processing error: {ex.Message}",
                    RequestId = HttpContext.TraceIdentifier
                });
            }
        }

        // If search model is not valid, return empty list of orders
        return View(new List<CustomerOrder>());
    }

    // Handle API errors
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
