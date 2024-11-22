namespace ThinClientApp.Pages.Tables;

using ThinClientApp.ViewModels.Tables;

public partial class OrderStatusesPage : ContentPage
{
    public OrderStatusesType()
    {
        InitializeComponent();
        BindingContext = new OrderStatusesViewModel();
    }
}