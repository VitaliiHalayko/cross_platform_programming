namespace ThinClientApp.Services;

using System.Net.Http.Json;
using DBWebApp.Models;

public class ShippingMethodsService
{
    private readonly HttpClient _httpClient;

    public ShippingMethodsService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<RefShippingMethod>> GetOrderStatusesAsync()
    {
        var apiBaseUrl = "/dbapi/Reference/RefOrderStatusesDB";
        var response = await _httpClient.GetFromJsonAsync<List<RefShippingMethod>>(apiBaseUrl);
        return response ?? new List<RefShippingMethod>();
    }
}