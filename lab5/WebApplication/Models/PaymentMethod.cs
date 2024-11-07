namespace WebApplication.Models
    
public class PaymentMethod
{
    public int PaymentMethodCode { get; set; }
    public string PaymentMethodDescription { get; set; }
    
    public ICollection<Customer> Customers { get; set; }
}