namespace ThinClientApp.Services;

using System.Net.Http.Json;
using DBWebApp.Models;

public class ProductsService
{
    private readonly HttpClient _httpClient;

    public ProductsService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<Product>> GetProductsAsync()
    {
        var apiBaseUrl = "/dbapi/Product/Product";
        var response = await _httpClient.GetFromJsonAsync<List<Product>>(apiBaseUrl);
        return response ?? new List<Product>();
    }
}