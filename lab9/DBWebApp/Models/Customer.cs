namespace DBWebApp.Models;

using System.ComponentModel.DataAnnotations;

public class Customer
{
    [Key]
    public int CustomerId { get; set; }
    public string PaymentMethodCode { get; set; }
    public string CustomerName { get; set; }
    public string CustomerPhone { get; set; }
    public string CustomerEmail { get; set; }
    public string CustomerAddress { get; set; }
    public string CustomerLogin { get; set; }
    public string CustomerPassword { get; set; }
    public string OtherCustomerDetails { get; set; }

    public RefPaymentMethod PaymentMethod { get; set; }
}