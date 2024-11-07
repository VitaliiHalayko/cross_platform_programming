namespace WebApplication.Models;

public class CustomerOrder
{
    public int OrderId { get; set; }
    public int CustomerId { get; set; }
    public Customer Customer { get; set; }
    
    public int OrderStatusCode { get; set; }
    public OrderStatus OrderStatus { get; set; }
    
    public int ShippingMethodCode { get; set; }
    public ShippingMethod ShippingMethod { get; set; }
    
    public DateTime OrderPlacedDatetime { get; set; }
    public decimal OrderShippingCharges { get; set; }
    public string OrderDetails { get; set; }
    
    public ICollection<OrderItem> OrderItems { get; set; }
}
