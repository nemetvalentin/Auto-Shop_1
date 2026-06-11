using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebShop.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddInStockToProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Putničke limuzine i sedan automobili.", "Sedani" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Sportsko-terenska vozila i četvorotokaši.", "SUV i terenski" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Sportski i super-sports automobili visokih performansi.", "Sportski automobili" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Karavan vozila, minivani i kombi vozila.", "Karavan i minivan" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Description", "Name", "Price" },
                values: new object[] { "Elegantna limuzina, 2.0L dizel, 190KS, automatski menjač, kamera za parkiranje.", "BMW Serija 5 520d", 52000m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Description", "Name", "Price" },
                values: new object[] { "Hibridni pogon, 218KS, premium oprema, Apple CarPlay, adaptive cruise control.", "Toyota Camry 2.5 Hybrid", 38500m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Description", "Name", "Price" },
                values: new object[] { "Luksuzni SUV, 3.0L dizel, 272KS, 4MATIC, panoramski krov, 7 sedišta.", "Mercedes-Benz GLE 350d", 75000m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Description", "Name", "Price" },
                values: new object[] { "Popularni porodični SUV, 150KS, DSG menjač, Digital Cockpit, LED svetla.", "Volkswagen Tiguan 2.0 TDI", 42000m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Description", "Name", "Price" },
                values: new object[] { "Kultni sportski automobil, 3.0L biturbo, 450KS, PDK, Sport Chrono paket.", "Porsche 911 Carrera 4S", 135000m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Description", "Name", "Price" },
                values: new object[] { "Sportski coupe, 3.0L biturbo, 510KS, M xDrive, Track mode, karbonski krov.", "BMW M4 Competition", 89000m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CategoryId", "Description", "Name", "Price" },
                values: new object[] { 4, "Praktičan karavan, 120KS, veliki prtljažnik 608L, Android Auto, sigurnosni sistemi.", "Ford Focus Karavan 1.5 EcoBlue", 28500m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "Description", "Name", "Price" },
                values: new object[] { "Moderan 7-sedišni minivan, 158KS, EDC menjač, panoramski krov, Google sistemi.", "Renault Espace 1.3 TCe", 45000m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CategoryId", "Description", "Name", "Price" },
                values: new object[] { 1, "Prestižna limuzina, 2.0L turbo, 245KS, quattro, Matrix LED, virtualna tabla.", "Audi A6 45 TFSI", 65000m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CategoryId", "Description", "Name", "Price" },
                values: new object[] { 1, "Električni automobil, domet 602km, Autopilot, 0-100 km/h za 4.4s, AWD.", "Tesla Model 3 Long Range", 48000m });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Desktop računari i radne stanice.", "Računari" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Prenosni računari svih kategorija.", "Laptopovi" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Procesori, matične ploče, RAM, grafičke kartice i ostale komponente.", "Komponente" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Tastature, miševi, monitori, slušalice i ostala periferija.", "Periferija" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Description", "Name", "Price" },
                values: new object[] { "Intel Core i7-13700K, 32GB DDR5, RTX 4070, 1TB NVMe SSD.", "Gaming Desktop PC", 1850m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Description", "Name", "Price" },
                values: new object[] { "Intel Core i5-12400, 16GB DDR4, 512GB SSD, Windows 11 Pro.", "Office Desktop PC", 750m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Description", "Name", "Price" },
                values: new object[] { "AMD Ryzen 7 7745H, 16GB RAM, RTX 4060, 1TB SSD, 144Hz ekran.", "Gaming Laptop 15.6\"", 1200m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Description", "Name", "Price" },
                values: new object[] { "Intel Core i5-1335U, 16GB RAM, 512GB SSD, Full HD IPS ekran.", "Business Laptop 14\"", 850m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Description", "Name", "Price" },
                values: new object[] { "NVIDIA GeForce RTX 4070, 12GB GDDR6X, PCIe 4.0.", "Grafička kartica RTX 4070", 620m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Description", "Name", "Price" },
                values: new object[] { "24-jezgarni procesor, do 5.8 GHz Turbo, LGA1700 socket.", "Procesor Intel Core i9-13900K", 430m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CategoryId", "Description", "Name", "Price" },
                values: new object[] { 3, "Kingston Fury Beast 32GB (2x16GB) DDR5, CL32.", "RAM 32GB DDR5 6000MHz", 140m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "Description", "Name", "Price" },
                values: new object[] { "IPS panel, 2560x1440, 165Hz, 1ms GTG, HDR400, FreeSync.", "Gaming Monitor 27\" 165Hz", 380m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CategoryId", "Description", "Name", "Price" },
                values: new object[] { 4, "TKL raspored, Cherry MX Red switches, RGB pozadinsko osvjetljenje.", "Mehanička tastatura", 95m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CategoryId", "Description", "Name", "Price" },
                values: new object[] { 4, "25600 DPI senzor, 7 programabilnih tastera, RGB, 70g težina.", "Gaming miš", 55m });
        }
    }
}
