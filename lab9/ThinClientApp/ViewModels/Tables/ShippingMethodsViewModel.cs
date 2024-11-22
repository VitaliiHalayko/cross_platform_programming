namespace ThinClientApp.ViewModels.Tables;

using DBWebApp.Models;
using ThinClientApp.Services;
using System.Collections.ObjectModel;

public class ShippingMethodsViewModel : BindableObject
{
    private readonly ShippingMethodsService _shippingMethodsService;
    private ObservableCollection<RefShippingMethod> _shippingMethodsList;

    public ObservableCollection<RefShippingMethod> ShippingMethodssList
    {
        get => _shippingMethodsList;
        set
        {
            _shippingMethodsList = value;
            OnPropertyChanged();
        }
    }

    public ShippingMethodsViewModel()
    {
        _shippingMethodsService = new ShippingMethodsService(new HttpClient());
        ShippingMethodsList = new ObservableCollection<RefShippingMethod>();
        LoadShippingMethods();
    }

    private async Task LoadShippingMethods()
    {
        var shippingMethods = await _shippingMethodsService.GetShippingMethodsAsync();
        ShippingMethodsList.Clear();
        foreach (var item in shippingMethods)
        {
            ShippingMethodsList.Add(item);
        }
    }
}