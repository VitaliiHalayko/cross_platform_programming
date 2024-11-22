namespace ThinClientApp.Pages;   

using Microcharts.Maui;
using ThinClientApp.Models;
using ThinClientApp.Services;
using SkiaSharp;
using System.Collections.Generic;
using System.Linq;
using Microcharts;
using ThinClientApp.ViewModels;

public partial class ChartPage : ContentPage
{
    public ChartPage()
    {
        InitializeComponent();
        BindingContext = new ChartViewModel();
    }
}