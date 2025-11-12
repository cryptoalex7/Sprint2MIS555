using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SkynetERP.Migrations
{
    /// <inheritdoc />
    public partial class UpdateFinancialManagementTablesWith8Entities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Budgets_Categories_CategoryId",
                table: "Budgets");

            migrationBuilder.DropForeignKey(
                name: "FK_Expenses_Accounts_AccountId",
                table: "Expenses");

            migrationBuilder.DropForeignKey(
                name: "FK_Expenses_Categories_CategoryId",
                table: "Expenses");

            migrationBuilder.DropForeignKey(
                name: "FK_Revenues_Accounts_AccountId",
                table: "Revenues");

            migrationBuilder.DropForeignKey(
                name: "FK_Revenues_Categories_CategoryId",
                table: "Revenues");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Accounts_AccountId",
                table: "Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Categories_CategoryId",
                table: "Transactions");

            migrationBuilder.DeleteData(
                table: "Budgets",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Budgets",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Budgets",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Budgets",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Budgets",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Budgets",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Expenses",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Expenses",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Expenses",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Expenses",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Expenses",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Expenses",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Expenses",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Revenues",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Revenues",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Revenues",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Revenues",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Revenues",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Revenues",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.AddColumn<int>(
                name: "PartnerId",
                table: "Payments",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PaymentType",
                table: "Payments",
                type: "varchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<decimal>(
                name: "Balance",
                table: "Invoices",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "InvoiceType",
                table: "Invoices",
                type: "varchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<decimal>(
                name: "PaidAmount",
                table: "Invoices",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "PartnerId",
                table: "Invoices",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "InvoiceLines",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    InvoiceId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Quantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TaxRate = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    LineTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Notes = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceLines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InvoiceLines_Invoices_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Invoices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "JournalEntries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    EntryNumber = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EntryDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Description = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Reference = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Status = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Notes = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedBy = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    PostedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JournalEntries", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Partners",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Type = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Phone = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Address = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    City = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    State = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ZipCode = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ContactPerson = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Notes = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Partners", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "TaxRates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Rate = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    TaxType = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Jurisdiction = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    EffectiveDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaxRates", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "JournalLines",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    JournalEntryId = table.Column<int>(type: "int", nullable: false),
                    AccountCode = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AccountName = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DebitAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreditAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Notes = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JournalLines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JournalLines_JournalEntries_JournalEntryId",
                        column: x => x.JournalEntryId,
                        principalTable: "JournalEntries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "AccountName", "AccountNumber", "AccountType", "Balance", "BankName", "CreatedAt", "Description", "IsActive" },
                values: new object[,]
                {
                    { 7, "Payroll Account", "****2468", "Checking", 45000.00m, "First National Bank", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Payroll processing account", true },
                    { 8, "Reserve Account", "****1357", "Savings", 100000.00m, "Commerce Bank", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Emergency reserve fund", true }
                });

            migrationBuilder.InsertData(
                table: "InvoiceLines",
                columns: new[] { "Id", "CreatedAt", "Description", "InvoiceId", "LineTotal", "Notes", "Quantity", "TaxRate", "UnitPrice" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Consulting Services - Q1", 1, 54250.00m, "Hourly consulting", 100m, 8.50m, 500.00m },
                    { 2, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Software License - Annual", 2, 75000.00m, "Annual license", 1m, 0m, 75000.00m },
                    { 3, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Support Package", 2, 10000.00m, "Premium support", 1m, 0m, 10000.00m },
                    { 4, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "IT Infrastructure Setup", 3, 27125.00m, "One-time setup", 1m, 8.50m, 25000.00m },
                    { 5, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Monthly Maintenance", 3, 8137.50m, "3 months maintenance", 3m, 8.50m, 2500.00m },
                    { 6, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Enterprise Service Package", 4, 120000.00m, "Annual contract", 1m, 0m, 120000.00m },
                    { 7, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Office Supplies - Bulk Order", 5, 16275.00m, "Bulk purchase", 50m, 8.50m, 300.00m },
                    { 8, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Product License - Enterprise", 6, 75000.00m, "5 licenses", 5m, 0m, 15000.00m },
                    { 9, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Training Services", 6, 21700.00m, "Training hours", 20m, 8.50m, 1000.00m },
                    { 10, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Setup Fee", 1, 5425.00m, "Initial setup", 1m, 8.50m, 5000.00m }
                });

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Balance", "InvoiceType", "PaidAmount", "PartnerId" },
                values: new object[] { 0.00m, "AR", 50000.00m, 1 });

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Balance", "InvoiceType", "PaidAmount", "PartnerId" },
                values: new object[] { 0.00m, "AR", 75000.00m, 2 });

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Balance", "CustomerPhone", "Description", "InvoiceType", "PaidAmount", "PartnerId" },
                values: new object[] { 35000.00m, "(555) 444-5555", "IT Services Invoice", "AP", 0.00m, 4 });

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Balance", "CustomerPhone", "InvoiceType", "PaidAmount", "PartnerId" },
                values: new object[] { 120000.00m, "(555) 555-6666", "AR", 0.00m, 5 });

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Balance", "CustomerEmail", "CustomerName", "CustomerPhone", "Description", "InvoiceType", "Notes", "PaidAmount", "PartnerId", "Status" },
                values: new object[] { 0.00m, "pay@officesupply.com", "Office Supply Co", "(555) 333-4444", "Office Supplies Invoice", "AP", "Payment completed", 15000.00m, 3, "Paid" });

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Balance", "InvoiceType", "PaidAmount", "PartnerId" },
                values: new object[] { 95000.00m, "AR", 0.00m, 6 });

            migrationBuilder.InsertData(
                table: "JournalEntries",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Description", "EntryDate", "EntryNumber", "Notes", "PostedAt", "Reference", "Status" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin", "Monthly Revenue Recognition", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "JE-2024-001", "Q1 revenue entry", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "REV-001", "Posted" },
                    { 2, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin", "Salary Accrual", new DateTime(2024, 1, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "JE-2024-002", "January payroll", new DateTime(2024, 1, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "SAL-001", "Posted" },
                    { 3, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin", "Depreciation Entry", new DateTime(2024, 2, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "JE-2024-003", "Monthly depreciation", new DateTime(2024, 2, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "DEP-001", "Posted" },
                    { 4, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin", "Accrued Expenses", new DateTime(2024, 3, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "JE-2024-004", "Q1 accruals", null, "ACC-001", "Draft" },
                    { 5, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin", "Revenue Adjustment", new DateTime(2024, 4, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "JE-2024-005", "Revenue correction", new DateTime(2024, 4, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "ADJ-001", "Posted" },
                    { 6, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin", "Month End Closing", new DateTime(2024, 4, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "JE-2024-006", "April closing entries", null, "CLOSE-001", "Draft" }
                });

            migrationBuilder.InsertData(
                table: "Partners",
                columns: new[] { "Id", "Address", "City", "ContactPerson", "CreatedAt", "Email", "IsActive", "Name", "Notes", "Phone", "State", "Type", "ZipCode" },
                values: new object[,]
                {
                    { 1, "123 Business St", "New York", "John Smith", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "contact@abccorp.com", true, "ABC Corporation", "Primary customer", "(555) 111-2222", "NY", "Customer", "10001" },
                    { 2, "456 Commerce Ave", "Los Angeles", "Jane Doe", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "info@xyzind.com", true, "XYZ Industries", "Regular customer", "(555) 222-3333", "CA", "Customer", "90001" },
                    { 3, "789 Vendor Blvd", "Chicago", "Bob Johnson", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "sales@officesupply.com", true, "Office Supply Co", "Office supplies vendor", "(555) 333-4444", "IL", "Vendor", "60601" },
                    { 4, "321 Tech Park", "Austin", "Alice Williams", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "contact@techsol.com", true, "Tech Solutions Inc", "IT services vendor", "(555) 444-5555", "TX", "Vendor", "73301" },
                    { 5, "654 Partnership Way", "Seattle", "Charlie Brown", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "billing@globalent.com", true, "Global Enterprises", "Customer and vendor", "(555) 555-6666", "WA", "Both", "98101" },
                    { 6, "987 Corporate Dr", "Boston", "David Lee", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "ap@megacorp.com", true, "Mega Corp", "Large enterprise customer", "(555) 666-7777", "MA", "Customer", "02101" }
                });

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "InvoiceId", "PartnerId", "PaymentType" },
                values: new object[] { 5, 3, "Outflow" });

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "PartnerId", "PaymentType" },
                values: new object[] { null, "Outflow" });

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Amount", "Description", "InvoiceId", "Notes", "PartnerId", "PayeeName", "PaymentDate", "PaymentMethod", "PaymentType", "ReferenceNumber" },
                values: new object[] { 50000.00m, "Customer payment", 1, "Invoice payment", 1, "ABC Corporation", new DateTime(2024, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Wire Transfer", "Inflow", "WT-003" });

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Amount", "Description", "InvoiceId", "Notes", "PartnerId", "PayeeName", "PaymentDate", "PaymentMethod", "PaymentType", "ReferenceNumber" },
                values: new object[] { 75000.00m, "Customer payment", 2, "Invoice payment", 2, "XYZ Industries", new DateTime(2024, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "ACH", "Inflow", "ACH-004" });

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Amount", "Description", "InvoiceId", "Notes", "PartnerId", "PayeeName", "PaymentDate", "PaymentType" },
                values: new object[] { 15000.00m, "Vendor payment", 3, "Partial payment", 4, "Tech Solutions Inc", new DateTime(2024, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Outflow" });

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Amount", "Description", "Notes", "PartnerId", "PayeeName", "PaymentDate", "PaymentMethod", "PaymentType", "ReferenceNumber", "Status" },
                values: new object[] { 12000.00m, "Marketing services", "Q1 campaign", null, "Marketing Agency", new DateTime(2024, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Check", "Outflow", "CHK-006", "Completed" });

            migrationBuilder.InsertData(
                table: "TaxRates",
                columns: new[] { "Id", "CreatedAt", "Description", "EffectiveDate", "ExpirationDate", "IsActive", "Jurisdiction", "Name", "Rate", "TaxType" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Standard state sales tax", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, "Kansas", "State Sales Tax", 8.50m, "Sales" },
                    { 2, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Corporate income tax rate", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, "Federal", "Federal Income Tax", 21.00m, "Income" },
                    { 3, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Local municipality tax", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, "Local", "Local Sales Tax", 2.00m, "Sales" },
                    { 4, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Value added tax for international", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, "International", "VAT", 20.00m, "VAT" },
                    { 5, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Service tax rate", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, "State", "Service Tax", 10.00m, "Service" },
                    { 6, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Business property tax", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, "Local", "Property Tax", 1.50m, "Property" }
                });

            migrationBuilder.InsertData(
                table: "JournalLines",
                columns: new[] { "Id", "AccountCode", "AccountName", "CreatedAt", "CreditAmount", "DebitAmount", "Description", "JournalEntryId", "Notes" },
                values: new object[,]
                {
                    { 1, "4000", "Revenue", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 150000.00m, 0m, "Product Sales", 1, "Revenue recognition" },
                    { 2, "1200", "Accounts Receivable", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0m, 150000.00m, "AR - Customer", 1, "AR entry" },
                    { 3, "5000", "Salaries Expense", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0m, 45000.00m, "January Salaries", 2, "Salary expense" },
                    { 4, "2100", "Accrued Salaries", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 45000.00m, 0m, "Salary Payable", 2, "Accrual" },
                    { 5, "6000", "Depreciation Expense", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0m, 5000.00m, "Equipment Depreciation", 3, "Monthly depreciation" },
                    { 6, "1500", "Accumulated Depreciation", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 5000.00m, 0m, "Accum Depreciation", 3, "Accumulated depreciation" },
                    { 7, "5100", "Utilities Expense", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0m, 3200.00m, "Accrued Utilities", 4, "Utility accrual" },
                    { 8, "2200", "Accrued Expenses", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3200.00m, 0m, "Accrued Utilities Payable", 4, "Accrued liability" },
                    { 9, "1200", "Accounts Receivable", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0m, 5000.00m, "AR Adjustment", 5, "Revenue adjustment" },
                    { 10, "4000", "Revenue", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 5000.00m, 0m, "Revenue Correction", 5, "Revenue correction" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Payments_PartnerId",
                table: "Payments",
                column: "PartnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_PartnerId",
                table: "Invoices",
                column: "PartnerId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceLines_InvoiceId",
                table: "InvoiceLines",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_JournalLines_JournalEntryId",
                table: "JournalLines",
                column: "JournalEntryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Budgets_Categories_CategoryId",
                table: "Budgets",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Expenses_Accounts_AccountId",
                table: "Expenses",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Expenses_Categories_CategoryId",
                table: "Expenses",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_Partners_PartnerId",
                table: "Invoices",
                column: "PartnerId",
                principalTable: "Partners",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Partners_PartnerId",
                table: "Payments",
                column: "PartnerId",
                principalTable: "Partners",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Revenues_Accounts_AccountId",
                table: "Revenues",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Revenues_Categories_CategoryId",
                table: "Revenues",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Accounts_AccountId",
                table: "Transactions",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Categories_CategoryId",
                table: "Transactions",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Budgets_Categories_CategoryId",
                table: "Budgets");

            migrationBuilder.DropForeignKey(
                name: "FK_Expenses_Accounts_AccountId",
                table: "Expenses");

            migrationBuilder.DropForeignKey(
                name: "FK_Expenses_Categories_CategoryId",
                table: "Expenses");

            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_Partners_PartnerId",
                table: "Invoices");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Partners_PartnerId",
                table: "Payments");

            migrationBuilder.DropForeignKey(
                name: "FK_Revenues_Accounts_AccountId",
                table: "Revenues");

            migrationBuilder.DropForeignKey(
                name: "FK_Revenues_Categories_CategoryId",
                table: "Revenues");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Accounts_AccountId",
                table: "Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Categories_CategoryId",
                table: "Transactions");

            migrationBuilder.DropTable(
                name: "InvoiceLines");

            migrationBuilder.DropTable(
                name: "JournalLines");

            migrationBuilder.DropTable(
                name: "Partners");

            migrationBuilder.DropTable(
                name: "TaxRates");

            migrationBuilder.DropTable(
                name: "JournalEntries");

            migrationBuilder.DropIndex(
                name: "IX_Payments_PartnerId",
                table: "Payments");

            migrationBuilder.DropIndex(
                name: "IX_Invoices_PartnerId",
                table: "Invoices");

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DropColumn(
                name: "PartnerId",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "PaymentType",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "Balance",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "InvoiceType",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "PaidAmount",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "PartnerId",
                table: "Invoices");

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedAt", "Description", "IsActive", "Name", "Type" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Revenue from product sales", true, "Product Sales", "Revenue" },
                    { 2, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Revenue from services", true, "Service Revenue", "Revenue" },
                    { 3, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Consulting service fees", true, "Consulting Fees", "Revenue" },
                    { 4, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Software licensing revenue", true, "Licensing Revenue", "Revenue" },
                    { 5, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Miscellaneous income", true, "Other Income", "Revenue" },
                    { 6, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Office supplies and materials", true, "Office Supplies", "Expense" },
                    { 7, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Employee salaries", true, "Salaries", "Expense" },
                    { 8, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Utility bills", true, "Utilities", "Expense" },
                    { 9, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Marketing and advertising", true, "Marketing", "Expense" },
                    { 10, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Office rent", true, "Rent", "Expense" },
                    { 11, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Business travel expenses", true, "Travel", "Expense" },
                    { 12, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Equipment purchases", true, "Equipment", "Expense" }
                });

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CustomerPhone", "Description" },
                values: new object[] { "(555) 333-4444", "Consulting Services" });

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: 4,
                column: "CustomerPhone",
                value: "(555) 444-5555");

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CustomerEmail", "CustomerName", "CustomerPhone", "Description", "Notes", "Status" },
                values: new object[] { "pay@startupco.com", "Startup Co", "(555) 555-6666", "Monthly Services", "Payment overdue", "Overdue" });

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "Id",
                keyValue: 1,
                column: "InvoiceId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Amount", "Description", "InvoiceId", "Notes", "PayeeName", "PaymentDate", "PaymentMethod", "ReferenceNumber" },
                values: new object[] { 15000.00m, "Marketing campaign payment", null, "Q1 campaign", "Marketing Agency", new DateTime(2024, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Credit Card", "CC-003" });

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Amount", "Description", "InvoiceId", "Notes", "PayeeName", "PaymentDate", "PaymentMethod", "ReferenceNumber" },
                values: new object[] { 12000.00m, "Office rent payment", null, "March rent", "Landlord", new DateTime(2024, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Check", "CHK-004" });

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Amount", "Description", "InvoiceId", "Notes", "PayeeName", "PaymentDate" },
                values: new object[] { 8500.00m, "Business travel payment", null, "Client meeting", "Travel Agency", new DateTime(2024, 3, 18, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Amount", "Description", "Notes", "PayeeName", "PaymentDate", "PaymentMethod", "ReferenceNumber", "Status" },
                values: new object[] { 25000.00m, "Equipment purchase payment", "New equipment", "Equipment Vendor", new DateTime(2024, 4, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Wire Transfer", "WT-006", "Pending" });

            migrationBuilder.InsertData(
                table: "Budgets",
                columns: new[] { "Id", "BudgetedAmount", "CategoryId", "CreatedAt", "EndDate", "IsActive", "Name", "Notes", "Period", "SpentAmount", "StartDate" },
                values: new object[,]
                {
                    { 1, 50000.00m, 9, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 3, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Q1 Marketing Budget", "First quarter marketing budget", "Quarterly", 15000.00m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, 5000.00m, 6, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 1, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Monthly Office Supplies", "January office supplies", "Monthly", 2500.00m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, 540000.00m, 7, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Annual Salaries", "Annual salary budget", "Yearly", 180000.00m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, 25000.00m, 11, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 6, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Q2 Travel Budget", "Second quarter travel", "Quarterly", 8500.00m, new DateTime(2024, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, 4000.00m, 8, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 2, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Monthly Utilities", "February utilities", "Monthly", 3200.00m, new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 6, 100000.00m, 12, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Equipment Budget", "Annual equipment purchases", "Yearly", 25000.00m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "AccountId", "Amount", "CategoryId", "CreatedAt", "CreatedBy", "Description", "Notes", "ReferenceNumber", "TransactionDate" },
                values: new object[,]
                {
                    { 1, 1, 2500.00m, 6, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin", "Office Supplies Purchase", "Monthly office supplies", "EXP-001", new DateTime(2024, 1, 8, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, 1, 45000.00m, 7, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin", "Employee Salaries", "January payroll", "EXP-002", new DateTime(2024, 1, 31, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, 1, 3200.00m, 8, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin", "Utility Bills", "Electricity and water", "EXP-003", new DateTime(2024, 2, 5, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, 1, 15000.00m, 9, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin", "Marketing Campaign", "Q1 marketing campaign", "EXP-004", new DateTime(2024, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, 1, 12000.00m, 10, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin", "Office Rent", "March office rent", "EXP-005", new DateTime(2024, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 6, 1, 8500.00m, 11, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin", "Business Travel", "Client meeting travel", "EXP-006", new DateTime(2024, 3, 18, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 7, 1, 25000.00m, 12, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin", "Equipment Purchase", "New office equipment", "EXP-007", new DateTime(2024, 4, 5, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Revenues",
                columns: new[] { "Id", "AccountId", "Amount", "CategoryId", "CreatedAt", "CreatedBy", "Description", "Notes", "ReferenceNumber", "TransactionDate" },
                values: new object[,]
                {
                    { 1, 1, 150000.00m, 1, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin", "Q1 Product Sales", "First quarter product sales", "REV-001", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, 1, 45000.00m, 3, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin", "Consulting Services", "Consulting project completion", "REV-002", new DateTime(2024, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, 2, 75000.00m, 2, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin", "Service Revenue", "Monthly service revenue", "REV-003", new DateTime(2024, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, 1, 120000.00m, 4, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin", "Software Licensing", "Annual licensing renewal", "REV-004", new DateTime(2024, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, 1, 95000.00m, 1, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin", "Product Sales", "April product sales", "REV-005", new DateTime(2024, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 6, 1, 5000.00m, 5, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin", "Other Income", "Miscellaneous income", "REV-006", new DateTime(2024, 4, 25, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Transactions",
                columns: new[] { "Id", "AccountId", "Amount", "CategoryId", "CreatedAt", "CreatedBy", "Description", "Notes", "ReferenceNumber", "Status", "TransactionDate", "Type" },
                values: new object[,]
                {
                    { 1, 1, 50000.00m, 1, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin", "Bank Transfer", "", "TXN-001", "Completed", new DateTime(2024, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Revenue" },
                    { 2, 1, -15000.00m, 9, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin", "Payment Processing", "", "TXN-002", "Completed", new DateTime(2024, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Expense" },
                    { 3, 2, 25000.00m, 2, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin", "Account Transfer", "", "TXN-003", "Completed", new DateTime(2024, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Revenue" },
                    { 4, 1, -8500.00m, 6, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin", "Vendor Payment", "", "TXN-004", "Pending", new DateTime(2024, 3, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Expense" },
                    { 5, 1, 35000.00m, 3, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin", "Client Payment", "", "TXN-005", "Completed", new DateTime(2024, 4, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Revenue" },
                    { 6, 1, -500.00m, 8, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin", "Service Fee", "", "TXN-006", "Completed", new DateTime(2024, 4, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Expense" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Budgets_Categories_CategoryId",
                table: "Budgets",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Expenses_Accounts_AccountId",
                table: "Expenses",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Expenses_Categories_CategoryId",
                table: "Expenses",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Revenues_Accounts_AccountId",
                table: "Revenues",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Revenues_Categories_CategoryId",
                table: "Revenues",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Accounts_AccountId",
                table: "Transactions",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Categories_CategoryId",
                table: "Transactions",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
