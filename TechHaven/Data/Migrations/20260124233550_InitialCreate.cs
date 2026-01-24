using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TechHaven.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DisplayName",
                table: "AspNetUsers",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    SpecsJson = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    StockQuantity = table.Column<int>(type: "int", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductSales",
                columns: table => new
                {
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductSales", x => new { x.OrderId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_ProductSales_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductSales_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WishlistItems",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    AddedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WishlistItems", x => new { x.UserId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_WishlistItems_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WishlistItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "ImageUrl", "Name" },
                values: new object[,]
                {
                    { 1, "/images/categories/cpu.jpg", "CPU" },
                    { 2, "/images/categories/gpu.jpg", "GPU" },
                    { 3, "/images/categories/motherboard.jpg", "Motherboard" },
                    { 4, "/images/categories/ram.jpg", "RAM" },
                    { 5, "/images/categories/storage.jpg", "Storage" },
                    { 6, "/images/categories/psu.jpg", "Power Supply" },
                    { 7, "/images/categories/cooling.jpg", "Cooling" },
                    { 8, "/images/categories/case.jpg", "Case" },
                    { 9, "/images/categories/peripheral.jpg", "Peripheral" },
                    { 10, "/images/categories/monitor.jpg", "Monitor" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "Description", "ImageUrl", "Name", "Price", "SpecsJson", "StockQuantity" },
                values: new object[,]
                {
                    { 1, 1, "6-core mid-range CPU with great single-thread performance.", "/images/products/intel_i5_12400f.jpg", "Intel Core i5-12400F", 229.99m, "{\"Cores\":6,\"Threads\":12,\"BaseClock\":\"2.5GHz\",\"BoostClock\":\"4.4GHz\",\"Socket\":\"LGA1700\",\"Manufacturer\":\"Intel\"}", 20 },
                    { 2, 1, "Affordable 6-core Ryzen CPU with excellent gaming performance.", "/images/products/amd_ryzen_5600x.jpg", "AMD Ryzen 5 5600X", 199.99m, "{\"Cores\":6,\"Threads\":12,\"BaseClock\":\"3.7GHz\",\"BoostClock\":\"4.6GHz\",\"Socket\":\"AM4\",\"Manufacturer\":\"AMD\"}", 15 },
                    { 3, 1, "12-core high-end CPU suitable for gaming and content creation.", "/images/products/intel_i7_12700k.jpg", "Intel Core i7-12700K", 699.99m, "{\"Cores\":12,\"Threads\":20,\"BaseClock\":\"3.6GHz\",\"BoostClock\":\"5.0GHz\",\"Socket\":\"LGA1700\",\"Manufacturer\":\"Intel\"}", 10 },
                    { 4, 2, "Great value GPU for 1080p & 1440p gaming.", "/images/products/asus_rtx3060.jpg", "ASUS TUF Gaming GeForce RTX 3060", 529.99m, "{\"VRAM\":\"12GB\",\"BoostClock\":\"1.78GHz\",\"PCIe\":\"4.0\",\"Brand\":\"Nvidia\",\"Manufacturer\":\"ASUS\"}", 10 },
                    { 5, 2, "High-end Nvidia GPU for 4K gaming and heavy workloads.", "/images/products/msi_rtx4080.jpg", "MSI GeForce RTX 4080 SUPER", 2699.99m, "{\"VRAM\":\"16GB\",\"BoostClock\":\"2.55GHz\",\"PCIe\":\"4.0\",\"Brand\":\"Nvidia\",\"Manufacturer\":\"MSI\"}", 5 },
                    { 6, 2, "Powerful AMD GPU for 1440p gaming with ray tracing.", "/images/products/gigabyte_6700xt.jpg", "Gigabyte Radeon RX 6700 XT", 679.99m, "{\"VRAM\":\"12GB\",\"BoostClock\":\"2.4GHz\",\"PCIe\":\"4.0\",\"Manufacturer\":\"Gigabyte\",\"Brand\":\"AMD\"}", 7 },
                    { 7, 3, "Low-budget AMD AM5 motherboard with very good price-to-performance ratio, capable of handling even more powerful CPUs.", "/images/products/asrock_b650m.jpg", "ASRock B650M-HDV/M.2", 169.99m, "{\"Socket\":\"AM5\",\"FormFactor\":\"Micro-ATX\",\"Chipset\":\"B650\"}", 12 },
                    { 8, 3, "Intel LGA1700 motherboard for 12th Gen CPUs with PCIe 5.0 support.", "/images/products/msi_z690a.jpg", "MSI Z690-A Pro", 259.99m, "{\"Socket\":\"LGA1700\",\"FormFactor\":\"ATX\",\"Chipset\":\"Z690\"}", 8 },
                    { 9, 3, "Affordable LGA1700 motherboard for mid-range builds.", "/images/products/gigabyte_b660m.jpg", "Gigabyte B660M DS3H", 129.99m, "{\"Socket\":\"LGA1700\",\"FormFactor\":\"Micro-ATX\",\"Chipset\":\"B660\"}", 15 },
                    { 10, 4, "High-speed DDR4 memory kit, 3200MHz.", "/images/products/corsair_16gb.jpg", "Corsair Vengeance LPX 16GB", 69.99m, "{\"Capacity\":\"16GB\",\"Type\":\"DDR4\",\"Speed\":\"3200MHz\"}", 25 },
                    { 11, 4, "Premium DDR4 RAM with RGB lighting, 3600MHz.", "/images/products/gskill_32gb.jpg", "G.Skill Trident Z RGB 32GB", 209.99m, "{\"Capacity\":\"32GB\",\"Type\":\"DDR4\",\"Speed\":\"3600MHz\"}", 18 },
                    { 12, 4, "Reliable DDR4 memory for gaming and multitasking.", "/images/products/kingston_16gb.jpg", "Kingston Fury Beast 16GB", 109.99m, "{\"Capacity\":\"16GB\",\"Type\":\"DDR4\",\"Speed\":\"3200MHz\"}", 30 },
                    { 13, 5, "High-speed NVMe SSD for gaming and productivity.", "/images/products/samsung_970evo.jpg", "Samsung 970 EVO Plus 250GB", 119.99m, "{\"Capacity\":\"250GB\",\"FormFactor\":\"M.2\",\"Interface\":\"NVMe\"}", 18 },
                    { 14, 5, "Reliable 2TB HDD for mass storage.", "/images/products/wd_2tb.jpg", "Western Digital Blue 2TB HDD", 59.99m, "{\"Capacity\":\"2TB\",\"FormFactor\":\"3.5in\",\"Interface\":\"SATA\"}", 25 },
                    { 15, 5, "High-performance NVMe SSD with PCIe 4.0 support.", "/images/products/crucial_p5plus.jpg", "Crucial P5 Plus 1TB", 129.99m, "{\"Capacity\":\"1TB\",\"FormFactor\":\"M.2\",\"Interface\":\"NVMe\"}", 20 },
                    { 16, 6, "750W 80+ Gold modular PSU with quiet operation.", "/images/products/corsair_rm750x.jpg", "Corsair RM750x", 169.99m, "{\"Wattage\":\"750W\",\"Efficiency\":\"80+ Gold\",\"Modular\":\"Yes\"}", 15 },
                    { 17, 6, "Reliable 650W 80+ Gold fully modular power supply.", "/images/products/evga_650g5.jpg", "EVGA SuperNOVA 650 G5", 99.99m, "{\"Wattage\":\"650W\",\"Efficiency\":\"80+ Gold\",\"Modular\":\"No\"}", 20 },
                    { 18, 6, "High-quality 850W PSU for high-end systems.", "/images/products/seasonic_850w.jpg", "Seasonic Focus GX-850", 239.99m, "{\"Wattage\":\"850W\",\"Efficiency\":\"80+ Gold\",\"Modular\":\"Yes\"}", 10 },
                    { 19, 7, "Low-budget single-tower air cooler for less demanding CPUs.", "/images/products/deepcool_ak400.jpg", "DeepCool AK400", 49.99m, "{\"Type\":\"Air Cooler\",\"Fans\":1}", 11 },
                    { 20, 7, "All-in-one liquid cooler with RGB lighting.", "/images/products/corsair_h100i.jpg", "Corsair iCUE H100i RGB", 169.99m, "{\"Type\":\"Liquid Cooler\",\"RadiatorSize\":\"240mm\"}", 10 },
                    { 21, 7, "Quiet and efficient air CPU cooler.", "/images/products/darkrock_pro4.jpg", "be quiet! Dark Rock Pro 4", 89.99m, "{\"Type\":\"Air Cooler\",\"Fans\":2}", 12 },
                    { 22, 8, "Mini-tower case with RGB fans, clean mesh design and exceptional airflow.", "/images/products/deepcool_ch360.jpg", "DeepCool CH360", 150.99m, "{\"FormFactor\":\"Mini Tower\",\"Color\":\"Black\"}", 20 },
                    { 23, 8, "High airflow mid-tower case with mesh front panel.", "/images/products/meshify_c.jpg", "Fractal Design Meshify C White", 120.99m, "{\"FormFactor\":\"Mid Tower\",\"Color\":\"White\"}", 15 },
                    { 24, 8, "Compact mid-tower case optimized for airflow.", "/images/products/corsair_4000d.jpg", "Corsair 4000D Airflow", 99.99m, "{\"FormFactor\":\"Mid Tower\",\"Color\":\"Black\"}", 18 },
                    { 25, 9, "Wired gaming mouse with adjustable DPI and 11 buttons.", "/images/products/logitech_g502.jpg", "Logitech G502 HERO", 109.99m, "{\"Type\":\"Mouse\",\"DPI\":\"16000\",\"Buttons\":11,\"Connectivity\":\"Wired\",\"RGB\":\"No\"}", 30 },
                    { 26, 9, "Mechanical gaming keyboard with Cherry MX switches.", "/images/products/corsair_k70.jpg", "Corsair K70 RGB MK.2", 129.99m, "{\"Type\":\"Keyboard\",\"Switch\":\"Cherry MX Red\",\"Keys\":104,\"Connectivity\":\"Wired\",\"RGB\":\"Yes\"}", 25 },
                    { 27, 9, "Ergonomic gaming mouse with high-precision optical sensor.", "/images/products/razer_deathadder_v2.jpg", "Razer DeathAdder V2", 69.99m, "{\"Type\":\"Mouse\",\"DPI\":\"20000\",\"Buttons\":8,\"Connectivity\":\"Wired\",\"RGB\":\"No\"}", 28 },
                    { 28, 10, "27-inch 4K IPS monitor with excellent color accuracy.", "/images/products/dell_u2720q.jpg", "Dell UltraSharp U2720Q", 499.99m, "{\"Size\":\"27in\",\"Resolution\":\"3840x2160\",\"Panel\":\"IPS\",\"RefreshRate\":\"60Hz\"}", 10 },
                    { 29, 10, "27-inch QHD gaming monitor with 165Hz refresh rate.", "/images/products/asus_vg27aq.jpg", "ASUS TUF Gaming VG27AQ", 399.99m, "{\"Size\":\"27in\",\"Resolution\":\"2560x1440\",\"Panel\":\"IPS\",\"RefreshRate\":\"165Hz\"}", 15 },
                    { 30, 10, "24-inch FHD gaming monitor with 180Hz refresh rate and a VA panel.", "/images/products/odyssey_g3.jpg", "Samsung Odyssey G3", 249.99m, "{\"Size\":\"24in\",\"Resolution\":\"1920x1080\",\"Panel\":\"VA\",\"RefreshRate\":\"180Hz\"}", 12 },
                    { 31, 9, "Tenkeyless mechanical gaming keyboard with Razer linear switches.", "/images/products/razer_huntsman.jpg", "Razer Hunstman V2 TKL", 169.99m, "{\"Type\":\"Keyboard\",\"Switch\":\"Razer\",\"Keys\":88,\"Connectivity\":\"Wired\",\"RGB\":\"Yes\"}", 25 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserId",
                table: "Orders",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSales_ProductId",
                table: "ProductSales",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_WishlistItems_ProductId",
                table: "WishlistItems",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductSales");

            migrationBuilder.DropTable(
                name: "WishlistItems");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropColumn(
                name: "DisplayName",
                table: "AspNetUsers");
        }
    }
}
