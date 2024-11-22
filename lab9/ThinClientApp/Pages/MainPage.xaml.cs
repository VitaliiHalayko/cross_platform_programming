namespace ThinClientApp.Pages;

using ThinClientApp.Models;

public partial class MainPage : ContentPage
{
    int count = 0;

    public MainPage()
    {
        InitializeComponent();
        BindingContext = new MainViewModel();
    }

    private async void OnButtonClicked(object sender, EventArgs e)
    {
        var viewModel = BindingContext as MainViewModel;
        if (viewModel == null) return;

        viewModel.IsBusy = true;

        await Task.Delay(1000);

        var button = sender as Button;
        if (button != null)
        {
            switch (button.Text)
            {
                case "Go to About Page":
                    await Navigation.PushAsync(new Pages.AboutPage());
                    break;
                case "Go to Chart Page":
                    await Navigation.PushAsync(new Pages.Chart());
                    break;
                case "Go to Products table Page":
                    await Navigation.PushAsync(new Pages.Tables.Products());
                    break;
                case "Go to OrderStatuses table Page":
                    await Navigation.PushAsync(new Pages.Tables.OrderStatuses());
                    break;
                case "Go to ShippingMethods table Page":
                    await Navigation.PushAsync(new Pages.Tables.ShippingMethods());
                    break;
                case "Log Out":
                    Application.Current.MainPage = new NavigationPage(new Pages.Login());
                    break;
            }
        }

        viewModel.IsBusy = false;
    }
}