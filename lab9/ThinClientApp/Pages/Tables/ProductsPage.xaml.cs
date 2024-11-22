namespace ThinClientApp.Pages.Tables;

using ThinClientApp.ViewModels.Tables;

public partial class ProductsPage : ContentPage
{
    public ProductsType()
    {
        InitializeComponent();
        BindingContext = new ProductsViewModel();
    }
}