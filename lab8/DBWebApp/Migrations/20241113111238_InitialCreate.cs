using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DBWebApp.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MailshotCampaigns",
                columns: table => new
                {
                    MailshotId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProductCategoryCode = table.Column<string>(type: "text", nullable: false),
                    MailshotName = table.Column<string>(type: "text", nullable: false),
                    MailshotStartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    MailshotEndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    MailshotTargetPopulation = table.Column<string>(type: "text", nullable: false),
                    MailshotObjectives = table.Column<string>(type: "text", nullable: false),
                    OtherMailshotDetails = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MailshotCampaigns", x => x.MailshotId);
                });

            migrationBuilder.CreateTable(
                name: "MailshotCustomers",
                columns: table => new
                {
                    MailshotId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CustomerId = table.Column<int>(type: "integer", nullable: false),
                    OutcodeCustomerDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MailshotCustomers", x => x.MailshotId);
                });

            migrationBuilder.CreateTable(
                name: "Premises",
                columns: table => new
                {
                    PremiseId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PremiseTypeCode = table.Column<string>(type: "text", nullable: false),
                    PremiseDetails = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Premises", x => x.PremiseId);
                });

            migrationBuilder.CreateTable(
                name: "RefAddressTypes",
                columns: table => new
                {
                    AddressTypeCode = table.Column<string>(type: "text", nullable: false),
                    AddressTypeDesc = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefAddressTypes", x => x.AddressTypeCode);
                });

            migrationBuilder.CreateTable(
                name: "RefOrderItemStatuses",
                columns: table => new
                {
                    OrderItemStatusCode = table.Column<string>(type: "text", nullable: false),
                    OrderItemStatusDesc = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefOrderItemStatuses", x => x.OrderItemStatusCode);
                });

            migrationBuilder.CreateTable(
                name: "RefOrderStatuses",
                columns: table => new
                {
                    OrderStatusCode = table.Column<string>(type: "text", nullable: false),
                    OrderStatusDesc = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefOrderStatuses", x => x.OrderStatusCode);
                });

            migrationBuilder.CreateTable(
                name: "RefOutcomeCodes",
                columns: table => new
                {
                    OutcomeCode = table.Column<string>(type: "text", nullable: false),
                    OutcomeDesc = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefOutcomeCodes", x => x.OutcomeCode);
                });

            migrationBuilder.CreateTable(
                name: "RefPaymentMethods",
                columns: table => new
                {
                    PaymentMethodCode = table.Column<string>(type: "text", nullable: false),
                    PaymentMethodDesc = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefPaymentMethods", x => x.PaymentMethodCode);
                });

            migrationBuilder.CreateTable(
                name: "RefPremisesTypes",
                columns: table => new
                {
                    PremisesTypeCode = table.Column<string>(type: "text", nullable: false),
                    PremisesTypeDesc = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefPremisesTypes", x => x.PremisesTypeCode);
                });

            migrationBuilder.CreateTable(
                name: "RefProductCategories",
                columns: table => new
                {
                    ProductCategoryCode = table.Column<string>(type: "text", nullable: false),
                    ProductCategoryDesc = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefProductCategories", x => x.ProductCategoryCode);
                });

            migrationBuilder.CreateTable(
                name: "RefShippingMethods",
                columns: table => new
                {
                    ShippingMethodCode = table.Column<string>(type: "text", nullable: false),
                    ShippingMethodDesc = table.Column<string>(type: "text", nullable: false),
                    ShippingCharges = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefShippingMethods", x => x.ShippingMethodCode);
                });

            migrationBuilder.CreateTable(
                name: "CustomerAddresses",
                columns: table => new
                {
                    CustomerId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PremiseId = table.Column<int>(type: "integer", nullable: false),
                    DateAddressFrom = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    AddressTypeCode = table.Column<string>(type: "text", nullable: false),
                    DateAddressTo = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    AddressTypeCode1 = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerAddresses", x => x.CustomerId);
                    table.ForeignKey(
                        name: "FK_CustomerAddresses_RefAddressTypes_AddressTypeCode1",
                        column: x => x.AddressTypeCode1,
                        principalTable: "RefAddressTypes",
                        principalColumn: "AddressTypeCode",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    CustomerId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PaymentMethodCode = table.Column<string>(type: "text", nullable: false),
                    CustomerName = table.Column<string>(type: "text", nullable: false),
                    CustomerPhone = table.Column<string>(type: "text", nullable: false),
                    CustomerEmail = table.Column<string>(type: "text", nullable: false),
                    CustomerAddress = table.Column<string>(type: "text", nullable: false),
                    CustomerLogin = table.Column<string>(type: "text", nullable: false),
                    CustomerPassword = table.Column<string>(type: "text", nullable: false),
                    OtherCustomerDetails = table.Column<string>(type: "text", nullable: false),
                    PaymentMethodCode1 = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.CustomerId);
                    table.ForeignKey(
                        name: "FK_Customers_RefPaymentMethods_PaymentMethodCode1",
                        column: x => x.PaymentMethodCode1,
                        principalTable: "RefPaymentMethods",
                        principalColumn: "PaymentMethodCode",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProductCategoryCode = table.Column<string>(type: "text", nullable: false),
                    ProductName = table.Column<string>(type: "text", nullable: false),
                    OtherProductDetails = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_Products_RefProductCategories_ProductCategoryCode",
                        column: x => x.ProductCategoryCode,
                        principalTable: "RefProductCategories",
                        principalColumn: "ProductCategoryCode",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CustomerOrders",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CustomerId = table.Column<int>(type: "integer", nullable: false),
                    OrderStatusCode = table.Column<string>(type: "text", nullable: false),
                    ShippingMethodCode = table.Column<string>(type: "text", nullable: false),
                    OrderPlacedDatetime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    OrderDeliveredDatetime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    OrderShippingCharges = table.Column<decimal>(type: "numeric", nullable: false),
                    OtherOrderDetails = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerOrders", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_CustomerOrders_RefOrderStatuses_OrderStatusCode",
                        column: x => x.OrderStatusCode,
                        principalTable: "RefOrderStatuses",
                        principalColumn: "OrderStatusCode",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomerOrders_RefShippingMethods_ShippingMethodCode",
                        column: x => x.ShippingMethodCode,
                        principalTable: "RefShippingMethods",
                        principalColumn: "ShippingMethodCode",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    ItemId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OrderItemStatusCode = table.Column<string>(type: "text", nullable: false),
                    OrderId = table.Column<int>(type: "integer", nullable: false),
                    ProductId = table.Column<int>(type: "integer", nullable: false),
                    ItemStatusCode = table.Column<string>(type: "text", nullable: false),
                    ItemDeliveredDatetime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ItemOrderQuantity = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.ItemId);
                    table.ForeignKey(
                        name: "FK_OrderItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItems_RefOrderItemStatuses_OrderItemStatusCode",
                        column: x => x.OrderItemStatusCode,
                        principalTable: "RefOrderItemStatuses",
                        principalColumn: "OrderItemStatusCode",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "RefOrderStatuses",
                columns: new[] { "OrderStatusCode", "OrderStatusDesc" },
                values: new object[,]
                {
                    { "Cancelled", "Cancelled" },
                    { "Delivered", "Delivered" },
                    { "Paid", "Paid" }
                });

            migrationBuilder.InsertData(
                table: "RefProductCategories",
                columns: new[] { "ProductCategoryCode", "ProductCategoryDesc" },
                values: new object[,]
                {
                    { "CLOTH", "Clothing" },
                    { "ELEC", "Electronics" }
                });

            migrationBuilder.InsertData(
                table: "RefShippingMethods",
                columns: new[] { "ShippingMethodCode", "ShippingCharges", "ShippingMethodDesc" },
                values: new object[,]
                {
                    { "FedEx", 10.0m, "FedEx" },
                    { "UPS", 15.0m, "UPS" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CustomerAddresses_AddressTypeCode1",
                table: "CustomerAddresses",
                column: "AddressTypeCode1");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerOrders_OrderStatusCode",
                table: "CustomerOrders",
                column: "OrderStatusCode");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerOrders_ShippingMethodCode",
                table: "CustomerOrders",
                column: "ShippingMethodCode");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_PaymentMethodCode1",
                table: "Customers",
                column: "PaymentMethodCode1");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderItemStatusCode",
                table: "OrderItems",
                column: "OrderItemStatusCode");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_ProductId",
                table: "OrderItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductCategoryCode",
                table: "Products",
                column: "ProductCategoryCode");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomerAddresses");

            migrationBuilder.DropTable(
                name: "CustomerOrders");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "MailshotCampaigns");

            migrationBuilder.DropTable(
                name: "MailshotCustomers");

            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "Premises");

            migrationBuilder.DropTable(
                name: "RefOutcomeCodes");

            migrationBuilder.DropTable(
                name: "RefPremisesTypes");

            migrationBuilder.DropTable(
                name: "RefAddressTypes");

            migrationBuilder.DropTable(
                name: "RefOrderStatuses");

            migrationBuilder.DropTable(
                name: "RefShippingMethods");

            migrationBuilder.DropTable(
                name: "RefPaymentMethods");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "RefOrderItemStatuses");

            migrationBuilder.DropTable(
                name: "RefProductCategories");
        }
    }
}
