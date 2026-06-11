using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebShop.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddOrdersAndUpdateSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ShippingAddress = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserId",
                table: "Orders",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderItems",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_ProductId",
                table: "OrderItems",
                column: "ProductId");

            // Update seed data - update categories
            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Code", "Description", "Name" },
                values: new object[] { "CAT-001", "Putničke limuzine i sedan automobili.", "Sedani" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Code", "Description", "Name" },
                values: new object[] { "CAT-002", "Sportsko-terenska vozila i četvorotokaši.", "SUV i terenski" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Code", "Description", "Name" },
                values: new object[] { "CAT-003", "Sportski i super-sports automobili visokih performansi.", "Sportski automobili" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Code", "Description", "Name" },
                values: new object[] { 4, "CAT-004", "Karavan vozila, minivani i kombi vozila.", "Karavan i minivan" });

            // Update existing products
            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CategoryId", "Code", "Description", "Name", "Price" },
                values: new object[] { 1, "PROD-001", "Elegantna limuzina, 2.0L dizel, 190KS, automatski menjač, kamera za parkiranje.", "BMW Serija 5 520d", 52000m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CategoryId", "Code", "Description", "Name", "Price" },
                values: new object[] { 1, "PROD-002", "Hibridni pogon, 218KS, premium oprema, Apple CarPlay, adaptive cruise control.", "Toyota Camry 2.5 Hybrid", 38500m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CategoryId", "Code", "Description", "Name", "Price" },
                values: new object[] { 2, "PROD-003", "Luksuzni SUV, 3.0L dizel, 272KS, 4MATIC, panoramski krov, 7 sedišta.", "Mercedes-Benz GLE 350d", 75000m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CategoryId", "Code", "Description", "Name", "Price" },
                values: new object[] { 2, "PROD-004", "Popularni porodični SUV, 150KS, DSG menjač, Digital Cockpit, LED svetla.", "Volkswagen Tiguan 2.0 TDI", 42000m });

            // Insert additional products
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "Code", "Description", "ImagePath", "Name", "Price" },
                values: new object[,]
                {
                    { 5, 3, "PROD-005", "Kultni sportski automobil, 3.0L biturbo, 450KS, PDK, Sport Chrono paket.", null, "Porsche 911 Carrera 4S", 135000m },
                    { 6, 3, "PROD-006", "Sportski coupe, 3.0L biturbo, 510KS, M xDrive, Track mode, karbonski krov.", null, "BMW M4 Competition", 89000m },
                    { 7, 4, "PROD-007", "Praktičan karavan, 120KS, veliki prtljažnik 608L, Android Auto, sigurnosni sistemi.", null, "Ford Focus Karavan 1.5 EcoBlue", 28500m },
                    { 8, 4, "PROD-008", "Moderan 7-sedišni minivan, 158KS, EDC menjač, panoramski krov, Google sistemi.", null, "Renault Espace 1.3 TCe", 45000m },
                    { 9, 1, "PROD-009", "Prestižna limuzina, 2.0L turbo, 245KS, quattro, Matrix LED, virtualna tabla.", null, "Audi A6 45 TFSI", 65000m },
                    { 10, 1, "PROD-010", "Električni automobil, domet 602km, Autopilot, 0-100 km/h za 4.4s, AWD.", null, "Tesla Model 3 Long Range", 48000m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "OrderItems");
            migrationBuilder.DropTable(name: "Orders");
        }
    }
}
