namespace Auth0.Controllers;

using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Auth0.Models;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IConfiguration _configuration;

    public AuthController(IHttpClientFactory httpClientFactory, IConfiguration configuration)
    {
        _httpClientFactory = httpClientFactory;
        _configuration = configuration;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var domain = _configuration["Auth0:Domain"];
        var clientId = _configuration["Auth0:ClientId"];
        var clientSecret = _configuration["Auth0:ClientSecret"];

        // request to Auth0
        var client = _httpClientFactory.CreateClient();
        var request = new HttpRequestMessage(HttpMethod.Post, $"https://{domain}/oauth/token");

        var payload = new
        {
            grant_type = "password",
            username = model.Username,
            password = model.Password,
            client_id = clientId,
            client_secret = clientSecret,
            scope = "openid profile email"
        };

        request.Content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");

        // send request
        var response = await client.SendAsync(request);
        var responseBody = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
            return BadRequest(JsonSerializer.Deserialize<object>(responseBody)); // return error

        // return token
        return Ok(JsonSerializer.Deserialize<object>(responseBody));
    }
    
    [HttpPost("logout")]
    public IActionResult Logout()
    {
        var domain = _configuration["Auth0:Domain"];
        var clientId = _configuration["Auth0:ClientId"];
        var clientSecret = _configuration["Auth0:ClientSecret"];
        
        var logoutUrl = $"https://{domain}/v2/logout?client_id={clientId}";
        
        // Optionally redirect to a post-logout URL
        var postLogoutRedirectUri = _configuration["Auth0:PostLogoutRedirectUri"];
        if (!string.IsNullOrEmpty(postLogoutRedirectUri))
        {
            logoutUrl += $"&returnTo={postLogoutRedirectUri}";
        }

        return Ok(new { message = "Logged out successfully", logoutUrl });
    }
}