namespace ThinClientApp.Pages;

using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using ThinClientApp.Models;

public partial class LoginPage : ContentPage
{
    public LoginPage()
    {
        InitializeComponent();
        BindingContext = this;
    }

    public string Username { get; set; }
    public string Password { get; set; }

    private async void LoginButton_Clicked(object sender, EventArgs e)
    {
        var client = new HttpClient();
        var payload = new
        {
            username = Username,
            password = Password
        };

        var json = JsonSerializer.Serialize(payload);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        try
        {
            var response = await client.PostAsync("http://localhost:5182/api/auth/login", content);
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var token = JsonSerializer.Deserialize<TokenResponse>(responseContent);
                await DisplayAlert("Success", "Logged in successfully!", "OK");

                // Navigate to Profile Page
                await Navigation.PushAsync(new ProfilePage(token));
            }
            else
            {
                await DisplayAlert("Error", "Invalid login credentials.", "OK");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "OK");
        }
    }
}

