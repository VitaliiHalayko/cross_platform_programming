namespace DBWebApp.Models;

using System.ComponentModel.DataAnnotations;

public class RefAddressType
{
    [Key]
    public string AddressTypeCode { get; set; }
    public string AddressTypeDesc { get; set; } // Billing, Shipping
}