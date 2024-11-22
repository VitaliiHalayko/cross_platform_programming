namespace ThinClientApp.ViewModels;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lab9.Services;
using Microcharts;
using SkiaSharp;
using System.ComponentModel;
using System.Runtime.CompilerServices;

public class ChartViewModel : INotifyPropertyChanged
{
    private readonly ProductsService _productsService;
    private Chart _chart;

    public Chart ChartData
    {
        get => _chart;
        set
        {
            _chart = value;
            OnPropertyChanged();
        }
    }

    public ChartViewModel()
    {
        _productsService = new ProductsService(new HttpClient());
        LoadChartData();
    }

    private async void LoadChartData()
    {
        var productsList = await _productsService.GetProductsAsync();

        int cat1Count = productsList.Count(s => s.ProductCategoryCode == "cat01");
        int cat2Count = productsList.Count(s => s.ProductCategoryCode == "cat02");

        var entries = new List<ChartEntry>
        {
            new ChartEntry(maleCount)
            {
                Label = "cat01",
                ValueLabel = cat1Count.ToString(),
                Color = SKColor.Parse("#FF6347")
            },
            new ChartEntry(femaleCount)
            {
                Label = "cat02",
                ValueLabel = cat2Count.ToString(),
                Color = SKColor.Parse("#87CEFA")
            }
        };

        ChartData = new PieChart { Entries = entries };
    }

    public event PropertyChangedEventHandler PropertyChanged;
    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}