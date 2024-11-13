namespace DBWebApp.Models;

using System.ComponentModel.DataAnnotations;

public class RefShippingMethod
{
    [Key]
    public string ShippingMethodCode { get; set; }
    public string ShippingMethodDesc { get; set; } // FedEx, UPS
    public decimal ShippingCharges { get; set; }
}