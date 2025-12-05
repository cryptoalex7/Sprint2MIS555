using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SkynetERP.Migrations
{
    /// <inheritdoc />
    public partial class AddInventoryManagementTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InventoryItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ItemName = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Category = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    ReorderLevel = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Supplier = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Location = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SKU = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LastUpdated = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatedBy = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryItems", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "CustomerReviews",
                keyColumn: "Id",
                keyValue: 1,
                column: "ReviewDate",
                value: new DateTime(2025, 11, 4, 19, 47, 11, 879, DateTimeKind.Local).AddTicks(9080));

            migrationBuilder.UpdateData(
                table: "CustomerReviews",
                keyColumn: "Id",
                keyValue: 2,
                column: "ReviewDate",
                value: new DateTime(2025, 10, 20, 19, 47, 11, 879, DateTimeKind.Local).AddTicks(9250));

            migrationBuilder.UpdateData(
                table: "CustomerReviews",
                keyColumn: "Id",
                keyValue: 3,
                column: "ReviewDate",
                value: new DateTime(2025, 11, 14, 19, 47, 11, 879, DateTimeKind.Local).AddTicks(9260));

            migrationBuilder.UpdateData(
                table: "CustomerReviews",
                keyColumn: "Id",
                keyValue: 4,
                column: "ReviewDate",
                value: new DateTime(2025, 11, 19, 19, 47, 11, 879, DateTimeKind.Local).AddTicks(9260));

            migrationBuilder.UpdateData(
                table: "CustomerReviews",
                keyColumn: "Id",
                keyValue: 5,
                column: "ReviewDate",
                value: new DateTime(2025, 11, 24, 19, 47, 11, 879, DateTimeKind.Local).AddTicks(9260));

            migrationBuilder.UpdateData(
                table: "CustomerReviews",
                keyColumn: "Id",
                keyValue: 6,
                column: "ReviewDate",
                value: new DateTime(2025, 11, 9, 19, 47, 11, 879, DateTimeKind.Local).AddTicks(9260));

            migrationBuilder.UpdateData(
                table: "CustomerReviews",
                keyColumn: "Id",
                keyValue: 7,
                column: "ReviewDate",
                value: new DateTime(2025, 11, 29, 19, 47, 11, 879, DateTimeKind.Local).AddTicks(9260));

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 1,
                column: "LastContact",
                value: new DateTime(2025, 11, 27, 19, 47, 11, 875, DateTimeKind.Local).AddTicks(2740));

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 2,
                column: "LastContact",
                value: new DateTime(2025, 12, 1, 19, 47, 11, 879, DateTimeKind.Local).AddTicks(6230));

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 3,
                column: "LastContact",
                value: new DateTime(2025, 11, 20, 19, 47, 11, 879, DateTimeKind.Local).AddTicks(6240));

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 4,
                column: "LastContact",
                value: new DateTime(2025, 11, 29, 19, 47, 11, 879, DateTimeKind.Local).AddTicks(6240));

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 5,
                column: "LastContact",
                value: new DateTime(2025, 12, 3, 19, 47, 11, 879, DateTimeKind.Local).AddTicks(6240));

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 6,
                column: "LastContact",
                value: new DateTime(2025, 9, 4, 19, 47, 11, 879, DateTimeKind.Local).AddTicks(6240));

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 7,
                column: "LastContact",
                value: new DateTime(2025, 12, 2, 19, 47, 11, 879, DateTimeKind.Local).AddTicks(6260));

            migrationBuilder.InsertData(
                table: "InventoryItems",
                columns: new[] { "Id", "Category", "CreatedAt", "CreatedBy", "Description", "ItemName", "LastUpdated", "Location", "Quantity", "ReorderLevel", "SKU", "Supplier", "UnitPrice" },
                values: new object[,]
                {
                    { 1, "Electronics", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "System", "High-performance laptop for business use", "Laptop Computer - Dell XPS 15", new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Warehouse A - Shelf 12", 25, 10, "LAP-DELL-XPS15-001", "Dell Technologies", 1299.99m },
                    { 2, "Furniture", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "System", "Ergonomic office chair with lumbar support", "Office Chair - Ergonomic", new DateTime(2024, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Warehouse B - Section 3", 8, 15, "FURN-CHAIR-ERG-001", "Office Furniture Co", 299.99m },
                    { 3, "Office Supplies", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "System", "Standard A4 printer paper, 500 sheets per ream", "Printer Paper - A4", new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Warehouse A - Shelf 5", 150, 50, "SUP-PAPER-A4-500", "Paper Supply Inc", 12.99m },
                    { 4, "Electronics", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "System", "Premium wireless mouse with advanced tracking", "Wireless Mouse - Logitech MX Master", new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Warehouse A - Shelf 8", 3, 20, "ELEC-MOUSE-LOG-MX-001", "Logitech", 99.99m },
                    { 5, "Furniture", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "System", "LED desk lamp with adjustable brightness", "Desk Lamp - LED", new DateTime(2024, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Warehouse B - Section 1", 0, 10, "FURN-LAMP-LED-001", "Lighting Solutions", 45.99m },
                    { 6, "Electronics", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "System", "High-speed USB-C charging and data cable", "USB-C Cable - 6ft", new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Warehouse A - Shelf 3", 75, 30, "ELEC-CABLE-USB-C-6FT", "Cable Pro", 15.99m },
                    { 7, "Office Supplies", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "System", "Spiral bound notebook, college ruled, 100 pages", "Notebook - Spiral Bound", new DateTime(2024, 10, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Warehouse A - Shelf 6", 200, 100, "SUP-NOTE-SPIRAL-100", "Paper Supply Inc", 4.99m },
                    { 8, "Furniture", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "System", "Adjustable monitor stand for dual monitor setup", "Monitor Stand - Adjustable", new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Warehouse B - Section 2", 12, 8, "FURN-STAND-MON-ADJ", "Office Furniture Co", 79.99m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InventoryItems");

            migrationBuilder.UpdateData(
                table: "CustomerReviews",
                keyColumn: "Id",
                keyValue: 1,
                column: "ReviewDate",
                value: new DateTime(2025, 11, 2, 13, 50, 34, 185, DateTimeKind.Local).AddTicks(7390));

            migrationBuilder.UpdateData(
                table: "CustomerReviews",
                keyColumn: "Id",
                keyValue: 2,
                column: "ReviewDate",
                value: new DateTime(2025, 10, 18, 13, 50, 34, 185, DateTimeKind.Local).AddTicks(7570));

            migrationBuilder.UpdateData(
                table: "CustomerReviews",
                keyColumn: "Id",
                keyValue: 3,
                column: "ReviewDate",
                value: new DateTime(2025, 11, 12, 13, 50, 34, 185, DateTimeKind.Local).AddTicks(7570));

            migrationBuilder.UpdateData(
                table: "CustomerReviews",
                keyColumn: "Id",
                keyValue: 4,
                column: "ReviewDate",
                value: new DateTime(2025, 11, 17, 13, 50, 34, 185, DateTimeKind.Local).AddTicks(7570));

            migrationBuilder.UpdateData(
                table: "CustomerReviews",
                keyColumn: "Id",
                keyValue: 5,
                column: "ReviewDate",
                value: new DateTime(2025, 11, 22, 13, 50, 34, 185, DateTimeKind.Local).AddTicks(7570));

            migrationBuilder.UpdateData(
                table: "CustomerReviews",
                keyColumn: "Id",
                keyValue: 6,
                column: "ReviewDate",
                value: new DateTime(2025, 11, 7, 13, 50, 34, 185, DateTimeKind.Local).AddTicks(7570));

            migrationBuilder.UpdateData(
                table: "CustomerReviews",
                keyColumn: "Id",
                keyValue: 7,
                column: "ReviewDate",
                value: new DateTime(2025, 11, 27, 13, 50, 34, 185, DateTimeKind.Local).AddTicks(7580));

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 1,
                column: "LastContact",
                value: new DateTime(2025, 11, 25, 13, 50, 34, 181, DateTimeKind.Local).AddTicks(2310));

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 2,
                column: "LastContact",
                value: new DateTime(2025, 11, 29, 13, 50, 34, 185, DateTimeKind.Local).AddTicks(4450));

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 3,
                column: "LastContact",
                value: new DateTime(2025, 11, 18, 13, 50, 34, 185, DateTimeKind.Local).AddTicks(4450));

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 4,
                column: "LastContact",
                value: new DateTime(2025, 11, 27, 13, 50, 34, 185, DateTimeKind.Local).AddTicks(4460));

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 5,
                column: "LastContact",
                value: new DateTime(2025, 12, 1, 13, 50, 34, 185, DateTimeKind.Local).AddTicks(4460));

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 6,
                column: "LastContact",
                value: new DateTime(2025, 9, 2, 13, 50, 34, 185, DateTimeKind.Local).AddTicks(4460));

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 7,
                column: "LastContact",
                value: new DateTime(2025, 11, 30, 13, 50, 34, 185, DateTimeKind.Local).AddTicks(4480));
        }
    }
}
