namespace DBWebApp.Models;

using System.ComponentModel.DataAnnotations;

public class RefProductCategory
{
    [Key]
    public string ProductCategoryCode { get; set; }
    public string ProductCategoryDesc { get; set; }
}