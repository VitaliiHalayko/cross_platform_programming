namespace WebApplication.Models;

public class OrderItem
{
    public int ItemId { get; set; }
    
    public int OrderId { get; set; }
    public CustomerOrder Order { get; set; }
    
    public int ProductId { get; set; }
    public Product Product { get; set; }
    
    public int OrderItemStatusCode { get; set; }
    public OrderItemStatus OrderItemStatus { get; set; }
    
    public int ItemOrderQuantity { get; set; }
}
