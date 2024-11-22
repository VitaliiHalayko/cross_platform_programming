namespace DBWebApp.Models;

using System.ComponentModel.DataAnnotations;

public class CustomerAddress
{
    [Key]
    public int CustomerId { get; set; }
    public int PremiseId { get; set; }
    public DateTime DateAddressFrom { get; set; }
    public string AddressTypeCode { get; set; }
    public DateTime DateAddressTo { get; set; }

    public RefAddressType AddressType { get; set; }
}