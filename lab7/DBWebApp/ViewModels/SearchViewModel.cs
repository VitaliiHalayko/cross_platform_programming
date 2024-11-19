namespace DBWebApp.ViewModels;

public class SearchViewModel
{
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public List<string> Statuses { get; set; } // all statuses
    public string ShippingMethodStart { get; set; } // first value for shipping method
    public string ShippingMethodEnd { get; set; }   // last value for shipping method
}