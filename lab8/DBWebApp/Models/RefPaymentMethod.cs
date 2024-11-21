namespace DBWebApp.Models;

using System.ComponentModel.DataAnnotations;

public class RefPaymentMethod
{
    [Key]
    public string PaymentMethodCode { get; set; }
    public string PaymentMethodDesc { get; set; }
}