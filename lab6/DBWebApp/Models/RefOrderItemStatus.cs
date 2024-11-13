namespace DBWebApp.Models;

using System.ComponentModel.DataAnnotations;

public class RefOrderItemStatus
{
    [Key]
    public string OrderItemStatusCode { get; set; }
    public string OrderItemStatusDesc { get; set; } // Cancelled, Delivered, Paid
}