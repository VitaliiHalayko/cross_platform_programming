namespace ThinClientApp.Services;

using System.Net.Http.Json;
using DBWebApp.Models;

public class OrderStatusesService
{
    private readonly HttpClient _httpClient;

    public OrderStatusesService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<RefOrderStatus>> GetOrderStatusesAsync()
    {
        var apiBaseUrl = "/dbapi/Reference/RefOrderStatusesDB";
        var response = await _httpClient.GetFromJsonAsync<List<RefOrderStatus>>(apiBaseUrl);
        return response ?? new List<RefOrderStatus>();
    }
}