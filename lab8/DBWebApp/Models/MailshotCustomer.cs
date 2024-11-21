namespace DBWebApp.Models;

using System.ComponentModel.DataAnnotations;

public class MailshotCustomer
{
    [Key]
    public int MailshotId { get; set; }
    public int CustomerId { get; set; }
    public DateTime OutcodeCustomerDate { get; set; }
}