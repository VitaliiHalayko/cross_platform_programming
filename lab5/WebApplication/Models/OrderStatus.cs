namespace WebApplication.Models;

public class OrderStatus
{
    public int OrderStatusCode { get; set; }
    public string OrderStatusDescription { get; set; }
    
    public ICollection<CustomerOrder> Orders { get; set; }
}