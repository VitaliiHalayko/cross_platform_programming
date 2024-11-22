namespace DBWebApp.Models;

using System.ComponentModel.DataAnnotations;

public class Product
{
    [Key]
    public int ProductId { get; set; }
    public string ProductCategoryCode { get; set; }
    public string ProductName { get; set; }
    public string OtherProductDetails { get; set; }

    public RefProductCategory ProductCategory { get; set; }
}