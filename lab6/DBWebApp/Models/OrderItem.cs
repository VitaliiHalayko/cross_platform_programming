namespace DBWebApp.Models;

using System.ComponentModel.DataAnnotations;

public class OrderItem
{
    [Key]
    public int ItemId { get; set; }
    public string OrderItemStatusCode { get; set; }
    public int OrderId { get; set; }
    public int ProductId { get; set; }
    public string ItemStatusCode { get; set; }
    public DateTime ItemDeliveredDatetime { get; set; }
    public int ItemOrderQuantity { get; set; }

    public Product Product { get; set; }
    public RefOrderItemStatus OrderItemStatus { get; set; }
}