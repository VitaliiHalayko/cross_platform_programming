namespace DBWebApp.Models;

using System.ComponentModel.DataAnnotations;

public class RefPremisesType
{
    [Key]
    public string PremisesTypeCode { get; set; }
    public string PremisesTypeDesc { get; set; } // Offices, Residence, Warehouse
}