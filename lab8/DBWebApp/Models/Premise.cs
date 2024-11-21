namespace DBWebApp.Models;

using System.ComponentModel.DataAnnotations;

public class Premise
{
    [Key]
    public int PremiseId { get; set; }
    public string PremiseTypeCode { get; set; }
    public string PremiseDetails { get; set; }
}