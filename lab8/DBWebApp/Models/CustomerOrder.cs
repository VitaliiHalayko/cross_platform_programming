namespace DBWebApp.Models;

using System.ComponentModel.DataAnnotations;

public class CustomerOrder
{
    [Key]
    public int OrderId { get; set; }
    public int CustomerId { get; set; }
    public string OrderStatusCode { get; set; }
    public string ShippingMethodCode { get; set; }
    public DateTime OrderPlacedDatetime { get; set; }
    public DateTime OrderDeliveredDatetime { get; set; }
    public decimal OrderShippingCharges { get; set; }
    public string OtherOrderDetails { get; set; }

    public RefOrderStatus OrderStatus { get; set; }
    public RefShippingMethod ShippingMethod { get; set; }
}