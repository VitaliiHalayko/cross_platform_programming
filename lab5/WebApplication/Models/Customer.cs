namespace WebApplication.Models

public class Customer
{
    public int CustomerId { get; set; }
    public string CustomerName { get; set; }
    public string CustomerEmail { get; set; }
    public string CustomerPassword { get; set; }

    public int PaymentMethodCode { get; set; }
    public PaymentMethod PaymentMethod { get; set; }

    public ICollection<CustomerAddress> Addresses { get; set; }
    public ICollection<CustomerOrder> Orders { get; set; }
}
