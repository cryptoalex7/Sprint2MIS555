using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SkynetERP.Migrations
{
    /// <inheritdoc />
    public partial class SeedTransactionAndCategoryData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Accounts_AccountId",
                table: "Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Categories_CategoryId",
                table: "Transactions");

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedAt", "Description", "IsActive", "Name", "Type" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Revenue from product sales", true, "Product Sales", "Revenue" },
                    { 2, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Revenue from services provided", true, "Service Revenue", "Revenue" },
                    { 3, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Revenue from consulting services", true, "Consulting Fees", "Revenue" },
                    { 4, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Expenses for office supplies and materials", true, "Office Supplies", "Expense" },
                    { 5, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Utility expenses (electricity, water, internet)", true, "Utilities", "Expense" },
                    { 6, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Employee salary expenses", true, "Salaries", "Expense" },
                    { 7, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Marketing and advertising expenses", true, "Marketing", "Expense" },
                    { 8, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Office rent and facility expenses", true, "Rent", "Expense" }
                });

            migrationBuilder.UpdateData(
                table: "CustomerReviews",
                keyColumn: "Id",
                keyValue: 1,
                column: "ReviewDate",
                value: new DateTime(2025, 11, 8, 22, 41, 29, 686, DateTimeKind.Local).AddTicks(6070));

            migrationBuilder.UpdateData(
                table: "CustomerReviews",
                keyColumn: "Id",
                keyValue: 2,
                column: "ReviewDate",
                value: new DateTime(2025, 10, 24, 22, 41, 29, 686, DateTimeKind.Local).AddTicks(6240));

            migrationBuilder.UpdateData(
                table: "CustomerReviews",
                keyColumn: "Id",
                keyValue: 3,
                column: "ReviewDate",
                value: new DateTime(2025, 11, 18, 22, 41, 29, 686, DateTimeKind.Local).AddTicks(6240));

            migrationBuilder.UpdateData(
                table: "CustomerReviews",
                keyColumn: "Id",
                keyValue: 4,
                column: "ReviewDate",
                value: new DateTime(2025, 11, 23, 22, 41, 29, 686, DateTimeKind.Local).AddTicks(6240));

            migrationBuilder.UpdateData(
                table: "CustomerReviews",
                keyColumn: "Id",
                keyValue: 5,
                column: "ReviewDate",
                value: new DateTime(2025, 11, 28, 22, 41, 29, 686, DateTimeKind.Local).AddTicks(6250));

            migrationBuilder.UpdateData(
                table: "CustomerReviews",
                keyColumn: "Id",
                keyValue: 6,
                column: "ReviewDate",
                value: new DateTime(2025, 11, 13, 22, 41, 29, 686, DateTimeKind.Local).AddTicks(6250));

            migrationBuilder.UpdateData(
                table: "CustomerReviews",
                keyColumn: "Id",
                keyValue: 7,
                column: "ReviewDate",
                value: new DateTime(2025, 12, 3, 22, 41, 29, 686, DateTimeKind.Local).AddTicks(6250));

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 1,
                column: "LastContact",
                value: new DateTime(2025, 12, 1, 22, 41, 29, 682, DateTimeKind.Local).AddTicks(2720));

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 2,
                column: "LastContact",
                value: new DateTime(2025, 12, 5, 22, 41, 29, 686, DateTimeKind.Local).AddTicks(3280));

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 3,
                column: "LastContact",
                value: new DateTime(2025, 11, 24, 22, 41, 29, 686, DateTimeKind.Local).AddTicks(3290));

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 4,
                column: "LastContact",
                value: new DateTime(2025, 12, 3, 22, 41, 29, 686, DateTimeKind.Local).AddTicks(3290));

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 5,
                column: "LastContact",
                value: new DateTime(2025, 12, 7, 22, 41, 29, 686, DateTimeKind.Local).AddTicks(3290));

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 6,
                column: "LastContact",
                value: new DateTime(2025, 9, 8, 22, 41, 29, 686, DateTimeKind.Local).AddTicks(3290));

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 7,
                column: "LastContact",
                value: new DateTime(2025, 12, 6, 22, 41, 29, 686, DateTimeKind.Local).AddTicks(3300));

            migrationBuilder.InsertData(
                table: "Transactions",
                columns: new[] { "Id", "AccountId", "Amount", "CategoryId", "CreatedAt", "CreatedBy", "Description", "Notes", "ReferenceNumber", "Status", "TransactionDate", "Type" },
                values: new object[,]
                {
                    { 1, 1, 50000.00m, 1, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin", "Product Sales - Q1 Revenue", "Q1 product sales revenue", "TXN-2024-001", "Completed", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Revenue" },
                    { 2, 1, 25000.00m, 3, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin", "Consulting Services Payment", "Consulting services revenue", "TXN-2024-002", "Completed", new DateTime(2024, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Revenue" },
                    { 3, 1, 3500.00m, 4, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin", "Office Supplies Purchase", "Monthly office supplies", "TXN-2024-003", "Completed", new DateTime(2024, 2, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Expense" },
                    { 4, 1, 3200.00m, 5, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin", "Utility Bill Payment", "Electricity and water bill", "TXN-2024-004", "Completed", new DateTime(2024, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Expense" },
                    { 5, 1, 75000.00m, 2, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin", "Service Revenue - Enterprise", "Enterprise service contract", "TXN-2024-005", "Completed", new DateTime(2024, 3, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Revenue" },
                    { 6, 1, 12000.00m, 7, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin", "Marketing Campaign Expense", "Q1 marketing campaign", "TXN-2024-006", "Completed", new DateTime(2024, 4, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Expense" },
                    { 7, 1, 8500.00m, 8, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin", "Office Rent Payment", "Monthly office rent", "TXN-2024-007", "Completed", new DateTime(2024, 4, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Expense" },
                    { 8, 1, 45000.00m, 1, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin", "Product Sales - Q2 Revenue", "Q2 product sales revenue", "TXN-2024-008", "Pending", new DateTime(2024, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Revenue" }
                });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Accounts_AccountId",
                table: "Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Categories_CategoryId",
                table: "Transactions");

            migrationBuilder.DeleteData(
                table: "Categories",
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
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 8);

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
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.UpdateData(
                table: "CustomerReviews",
                keyColumn: "Id",
                keyValue: 1,
                column: "ReviewDate",
                value: new DateTime(2025, 11, 8, 22, 29, 18, 479, DateTimeKind.Local).AddTicks(9220));

            migrationBuilder.UpdateData(
                table: "CustomerReviews",
                keyColumn: "Id",
                keyValue: 2,
                column: "ReviewDate",
                value: new DateTime(2025, 10, 24, 22, 29, 18, 479, DateTimeKind.Local).AddTicks(9390));

            migrationBuilder.UpdateData(
                table: "CustomerReviews",
                keyColumn: "Id",
                keyValue: 3,
                column: "ReviewDate",
                value: new DateTime(2025, 11, 18, 22, 29, 18, 479, DateTimeKind.Local).AddTicks(9390));

            migrationBuilder.UpdateData(
                table: "CustomerReviews",
                keyColumn: "Id",
                keyValue: 4,
                column: "ReviewDate",
                value: new DateTime(2025, 11, 23, 22, 29, 18, 479, DateTimeKind.Local).AddTicks(9390));

            migrationBuilder.UpdateData(
                table: "CustomerReviews",
                keyColumn: "Id",
                keyValue: 5,
                column: "ReviewDate",
                value: new DateTime(2025, 11, 28, 22, 29, 18, 479, DateTimeKind.Local).AddTicks(9400));

            migrationBuilder.UpdateData(
                table: "CustomerReviews",
                keyColumn: "Id",
                keyValue: 6,
                column: "ReviewDate",
                value: new DateTime(2025, 11, 13, 22, 29, 18, 479, DateTimeKind.Local).AddTicks(9400));

            migrationBuilder.UpdateData(
                table: "CustomerReviews",
                keyColumn: "Id",
                keyValue: 7,
                column: "ReviewDate",
                value: new DateTime(2025, 12, 3, 22, 29, 18, 479, DateTimeKind.Local).AddTicks(9400));

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 1,
                column: "LastContact",
                value: new DateTime(2025, 12, 1, 22, 29, 18, 474, DateTimeKind.Local).AddTicks(9940));

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 2,
                column: "LastContact",
                value: new DateTime(2025, 12, 5, 22, 29, 18, 479, DateTimeKind.Local).AddTicks(6190));

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 3,
                column: "LastContact",
                value: new DateTime(2025, 11, 24, 22, 29, 18, 479, DateTimeKind.Local).AddTicks(6200));

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 4,
                column: "LastContact",
                value: new DateTime(2025, 12, 3, 22, 29, 18, 479, DateTimeKind.Local).AddTicks(6200));

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 5,
                column: "LastContact",
                value: new DateTime(2025, 12, 7, 22, 29, 18, 479, DateTimeKind.Local).AddTicks(6200));

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 6,
                column: "LastContact",
                value: new DateTime(2025, 9, 8, 22, 29, 18, 479, DateTimeKind.Local).AddTicks(6210));

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 7,
                column: "LastContact",
                value: new DateTime(2025, 12, 6, 22, 29, 18, 479, DateTimeKind.Local).AddTicks(6220));

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
    }
}
