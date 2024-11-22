namespace ThinClientApp.ViewModels.Tables;

using DBWebApp.Models;
using ThinClientApp.Services;
using System.Collections.ObjectModel;

public class ProductsViewModel : BindableObject
{
    private readonly ProductsService _productsService;
    private ObservableCollection<Product> _productsList;

    public ObservableCollection<Product> ProductssList
    {
        get => _productsList;
        set
        {
            _productsList = value;
            OnPropertyChanged();
        }
    }

    public ProductsViewModel()
    {
        _productsService = new ProductsService(new HttpClient());
        ProductsList = new ObservableCollection<Product>();
        LoadProducts();
    }

    private async Task LoadProductss()
    {
        var products = await _productsService.GetProductsAsync();
        ProductsList.Clear();
        foreach (var item in products)
        {
            ProductsList.Add(item);
        }
    }
}