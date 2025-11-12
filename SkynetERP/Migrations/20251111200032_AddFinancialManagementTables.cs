using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SkynetERP.Migrations
{
    /// <inheritdoc />
    public partial class AddFinancialManagementTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AccountName = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AccountType = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    BankName = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AccountNumber = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Balance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Description = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Type = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Invoices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    InvoiceNumber = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CustomerName = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    InvoiceDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DueDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Status = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CustomerEmail = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CustomerPhone = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Notes = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoices", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Budgets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    BudgetedAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SpentAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Period = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Notes = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Budgets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Budgets_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Expenses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TransactionDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    AccountId = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    ReferenceNumber = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Notes = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedBy = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Expenses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Expenses_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Expenses_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Revenues",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TransactionDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    AccountId = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    ReferenceNumber = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Notes = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedBy = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Revenues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Revenues_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Revenues_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Type = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TransactionDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    AccountId = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: true),
                    ReferenceNumber = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Notes = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Status = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedBy = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transactions_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Transactions_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    PaymentNumber = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PayeeName = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    AccountId = table.Column<int>(type: "int", nullable: false),
                    InvoiceId = table.Column<int>(type: "int", nullable: true),
                    PaymentMethod = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Status = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ReferenceNumber = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Notes = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedBy = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Payments_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Payments_Invoices_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Invoices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "AccountName", "AccountNumber", "AccountType", "Balance", "BankName", "CreatedAt", "Description", "IsActive" },
                values: new object[,]
                {
                    { 1, "Main Checking", "****1234", "Checking", 125000.00m, "First National Bank", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Primary business checking account", true },
                    { 2, "Savings Account", "****5678", "Savings", 250000.00m, "First National Bank", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Business savings account", true },
                    { 3, "Operating Account", "****9012", "Checking", 75000.00m, "Commerce Bank", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Daily operations account", true },
                    { 4, "Credit Card", "****3456", "Credit", -5000.00m, "Business Credit", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Business credit card", true },
                    { 5, "Petty Cash", "N/A", "Cash", 5000.00m, "N/A", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Office petty cash fund", true },
                    { 6, "Investment Account", "****7890", "Investment", 500000.00m, "Investment Bank", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Long-term investment account", true }
                });

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

            migrationBuilder.InsertData(
                table: "Invoices",
                columns: new[] { "Id", "Amount", "CreatedAt", "CustomerEmail", "CustomerName", "CustomerPhone", "Description", "DueDate", "InvoiceDate", "InvoiceNumber", "Notes", "Status" },
                values: new object[,]
                {
                    { 1, 50000.00m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "billing@abccorp.com", "ABC Corporation", "(555) 111-2222", "Q1 Service Package", new DateTime(2024, 2, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "INV-2024-001", "Payment received", "Paid" },
                    { 2, 75000.00m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "finance@xyzind.com", "XYZ Industries", "(555) 222-3333", "Software Licensing", new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "INV-2024-002", "Paid on time", "Paid" },
                    { 3, 35000.00m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "accounts@techsol.com", "Tech Solutions Inc", "(555) 333-4444", "Consulting Services", new DateTime(2024, 4, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "INV-2024-003", "Awaiting payment", "Pending" },
                    { 4, 120000.00m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "billing@globalent.com", "Global Enterprises", "(555) 444-5555", "Annual Service Contract", new DateTime(2024, 4, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "INV-2024-004", "Large contract", "Pending" },
                    { 5, 15000.00m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "pay@startupco.com", "Startup Co", "(555) 555-6666", "Monthly Services", new DateTime(2024, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "INV-2024-005", "Payment overdue", "Overdue" },
                    { 6, 95000.00m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "ap@megacorp.com", "Mega Corp", "(555) 666-7777", "Product Purchase", new DateTime(2024, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 4, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "INV-2024-006", "Awaiting approval", "Pending" }
                });

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
                table: "Payments",
                columns: new[] { "Id", "AccountId", "Amount", "CreatedAt", "CreatedBy", "Description", "InvoiceId", "Notes", "PayeeName", "PaymentDate", "PaymentMethod", "PaymentNumber", "ReferenceNumber", "Status" },
                values: new object[,]
                {
                    { 1, 1, 2500.00m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin", "Office supplies payment", null, "Monthly supplies", "Office Supply Co", new DateTime(2024, 1, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "Check", "PAY-2024-001", "CHK-001", "Completed" },
                    { 2, 1, 3200.00m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin", "Utility bill payment", null, "Electricity and water", "Utility Company", new DateTime(2024, 2, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Wire Transfer", "PAY-2024-002", "WT-002", "Completed" },
                    { 3, 1, 15000.00m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin", "Marketing campaign payment", null, "Q1 campaign", "Marketing Agency", new DateTime(2024, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Credit Card", "PAY-2024-003", "CC-003", "Completed" },
                    { 4, 1, 12000.00m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin", "Office rent payment", null, "March rent", "Landlord", new DateTime(2024, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Check", "PAY-2024-004", "CHK-004", "Completed" },
                    { 5, 1, 8500.00m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin", "Business travel payment", null, "Client meeting", "Travel Agency", new DateTime(2024, 3, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "Credit Card", "PAY-2024-005", "CC-005", "Completed" },
                    { 6, 1, 25000.00m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin", "Equipment purchase payment", null, "New equipment", "Equipment Vendor", new DateTime(2024, 4, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Wire Transfer", "PAY-2024-006", "WT-006", "Pending" }
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

            migrationBuilder.CreateIndex(
                name: "IX_Budgets_CategoryId",
                table: "Budgets",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_AccountId",
                table: "Expenses",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_CategoryId",
                table: "Expenses",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_AccountId",
                table: "Payments",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_InvoiceId",
                table: "Payments",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Revenues_AccountId",
                table: "Revenues",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Revenues_CategoryId",
                table: "Revenues",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_AccountId",
                table: "Transactions",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_CategoryId",
                table: "Transactions",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Budgets");

            migrationBuilder.DropTable(
                name: "Expenses");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "Revenues");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "Invoices");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
