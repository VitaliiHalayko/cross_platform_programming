namespace DBWebApp.Models;

using System.ComponentModel.DataAnnotations;

public class MailshotCampaign
{
    [Key]
    public int MailshotId { get; set; }
    public string ProductCategoryCode { get; set; }
    public string MailshotName { get; set; }
    public DateTime MailshotStartDate { get; set; }
    public DateTime MailshotEndDate { get; set; }
    public string MailshotTargetPopulation { get; set; }
    public string MailshotObjectives { get; set; }
    public string OtherMailshotDetails { get; set; }
}