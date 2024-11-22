namespace ThinClientApp.ViewModels.Tables;

using DBWebApp.Models;
using ThinClientApp.Services;
using System.Collections.ObjectModel;

public class OrderStatusesViewModel : BindableObject
{
    private readonly OrderStatusesService _orderStatusesService;
    private ObservableCollection<RefOrderStatus> _orderStatusesList;

    public ObservableCollection<RefOrderStatus> OrderStatusessList
    {
        get => _orderStatusesList;
        set
        {
            _orderStatusesList = value;
            OnPropertyChanged();
        }
    }

    public OrderStatusesViewModel()
    {
        _orderStatusesService = new OrderStatusesService(new HttpClient());
        OrderStatusesList = new ObservableCollection<RefOrderStatus>();
        LoadOrderStatuses();
    }

    private async Task LoadOrderStatuses()
    {
        var orderStatuses = await _orderStatusesService.GetOrderStatusesAsync();
        OrderStatusesList.Clear();
        foreach (var item in orderStatuses)
        {
            OrderStatusesList.Add(item);
        }
    }
}