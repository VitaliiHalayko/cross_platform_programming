namespace DBWebApp.Models;

using System.ComponentModel.DataAnnotations;

public class RefOrderStatus
{
    [Key]
    public string OrderStatusCode { get; set; }
    public string OrderStatusDesc { get; set; } // Cancelled, Delivered, Paid
}