namespace ThinClientApp.Pages.Tables;

using ThinClientApp.ViewModels.Tables;

public partial class ShippingMethodsPage : ContentPage
{
    public ShippingMethodsType()
    {
        InitializeComponent();
        BindingContext = new ShippingMethodsViewModel();
    }
}