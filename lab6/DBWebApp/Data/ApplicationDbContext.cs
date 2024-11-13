namespace DBWebApp.Data;

using Microsoft.EntityFrameworkCore;
using DBWebApp.Models;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IConfiguration configuration)
        : base(options)
    {
    }
    
    public DbSet<RefOrderStatus> RefOrderStatuses { get; set; }
    public DbSet<RefShippingMethod> RefShippingMethods { get; set; }
    public DbSet<RefProductCategory> RefProductCategories { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<RefOrderItemStatus> RefOrderItemStatuses { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<CustomerOrder> CustomerOrders { get; set; }
    public DbSet<RefPaymentMethod> RefPaymentMethods { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<CustomerAddress> CustomerAddresses { get; set; }
    public DbSet<Premise> Premises { get; set; }
    public DbSet<MailshotCampaign> MailshotCampaigns { get; set; }
    public DbSet<MailshotCustomer> MailshotCustomers { get; set; }
    public DbSet<RefOutcomeCode> RefOutcomeCodes { get; set; }
    public DbSet<RefAddressType> RefAddressTypes { get; set; }
    public DbSet<RefPremisesType> RefPremisesTypes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var dbConfiguration = new DbConfiguration
        {
            DatabaseProvider = "PostgreSQL", // Example: switch to MS-SQL if needed
            ConnectionString = "Host=localhost;Database=kpp;Username=postgres;Password=postgres"
        };

        // You already have optionsBuilder as a parameter, no need to redefine it here
        switch (dbConfiguration.DatabaseProvider)
        {
            case "SqlServer":
                optionsBuilder.UseSqlServer(dbConfiguration.ConnectionString);
                break;
            case "PostgreSQL":
                optionsBuilder.UseNpgsql(dbConfiguration.ConnectionString);
                break;
            case "Sqlite":
                optionsBuilder.UseSqlite(dbConfiguration.ConnectionString);
                break;
            case "InMemory":
                optionsBuilder.UseInMemoryDatabase("InMemoryDb");
                break;
            default:
                throw new Exception("Unsupported database provider.");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Define relationships between tables
        modelBuilder.Entity<Product>()
            .HasOne(p => p.ProductCategory)
            .WithMany()
            .HasForeignKey(p => p.ProductCategoryCode);

        modelBuilder.Entity<OrderItem>()
            .HasOne(o => o.OrderItemStatus)
            .WithMany()
            .HasForeignKey(o => o.OrderItemStatusCode);

        modelBuilder.Entity<CustomerOrder>()
            .HasOne(co => co.OrderStatus)
            .WithMany()
            .HasForeignKey(co => co.OrderStatusCode);

        modelBuilder.Entity<CustomerOrder>()
            .HasOne(co => co.ShippingMethod)
            .WithMany()
            .HasForeignKey(co => co.ShippingMethodCode);

        // Seed initial data
        modelBuilder.Entity<RefOrderStatus>().HasData(
            new RefOrderStatus { OrderStatusCode = "Cancelled", OrderStatusDesc = "Cancelled" },
            new RefOrderStatus { OrderStatusCode = "Delivered", OrderStatusDesc = "Delivered" },
            new RefOrderStatus { OrderStatusCode = "Paid", OrderStatusDesc = "Paid" }
        );

        modelBuilder.Entity<RefShippingMethod>().HasData(
            new RefShippingMethod
                { ShippingMethodCode = "FedEx", ShippingMethodDesc = "FedEx", ShippingCharges = 10.0m },
            new RefShippingMethod { ShippingMethodCode = "UPS", ShippingMethodDesc = "UPS", ShippingCharges = 15.0m }
        );

        modelBuilder.Entity<RefProductCategory>().HasData(
            new RefProductCategory { ProductCategoryCode = "ELEC", ProductCategoryDesc = "Electronics" },
            new RefProductCategory { ProductCategoryCode = "CLOTH", ProductCategoryDesc = "Clothing" }
        );
    }
}
