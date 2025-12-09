using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SkynetERP.Migrations
{
    /// <inheritdoc />
    public partial class AddRoleBasedTestAccounts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "JAvlGPq9JyTdtvBO6x2llnRI1+gxwIyPqCKAn3THIKk=");

            // Insert new users only if they don't exist (by email)
            migrationBuilder.Sql(@"
                INSERT IGNORE INTO `Users` (`Id`, `CreatedAt`, `Email`, `FirstName`, `LastName`, `Password`, `Role`, `Username`)
                VALUES 
                (5, '2024-01-01 00:00:00', 'guest@erp.com', 'Guest', 'User', 'a5PMukFKwdCuHnfz+sVgx0imcB7WlGc1pJ1GM1FRjhY=', 'Guest', 'guest'),
                (6, '2024-01-01 00:00:00', 'customer@erp.com', 'Customer', 'User', 'sEHArrNbsPpKpmjKWpILWQGW/a+aAOuFLJt/TRI8xtY=', 'Customer', 'customer'),
                (7, '2024-01-01 00:00:00', 'accountant@erp.com', 'Accountant', 'User', 'TTk+w0w8aodbleZt9ebW/Anvwz1m8S4+mK/KNH1rdjg=', 'Accountant', 'accountant'),
                (8, '2024-01-01 00:00:00', 'inventory@erp.com', 'Inventory', 'Manager', 'zWPvJx+fXIHDrJ4k5UT36YI2DrwCe/TmtpYEhbE/iec=', 'InventoryManager', 'inventory')
                ON DUPLICATE KEY UPDATE 
                    `Email` = VALUES(`Email`),
                    `FirstName` = VALUES(`FirstName`),
                    `LastName` = VALUES(`LastName`),
                    `Password` = VALUES(`Password`),
                    `Role` = VALUES(`Role`),
                    `Username` = VALUES(`Username`);
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 8);

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

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "XohImNooBHFR0OVvjcYpJ3NgPQ1qq73WKhHvch0VQtg=");
        }
    }
}
