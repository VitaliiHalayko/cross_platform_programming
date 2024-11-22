namespace DBWebApp.Models;

using System.ComponentModel.DataAnnotations;

public class RefOutcomeCode
{
    [Key]
    public string OutcomeCode { get; set; }
    public string OutcomeDesc { get; set; } // No Resp, Order obtained
}