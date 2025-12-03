using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SkynetERP.Migrations
{
    /// <inheritdoc />
    public partial class AddCRMSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "Company", "CreatedAt", "Email", "LastContact", "Name", "Notes", "Phone", "Status" },
                values: new object[,]
                {
                    { 1, "TechCorp Solutions", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "john.smith@techcorp.com", new DateTime(2025, 11, 25, 13, 50, 34, 181, DateTimeKind.Local).AddTicks(2310), "John Smith", "Primary contact for enterprise solutions. Very responsive.", "(555) 111-2222", "Active" },
                    { 2, "Global Industries Inc", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "sarah.j@globalind.com", new DateTime(2025, 11, 29, 13, 50, 34, 185, DateTimeKind.Local).AddTicks(4450), "Sarah Johnson", "Regular customer, quarterly orders. Excellent payment history.", "(555) 222-3333", "Active" },
                    { 3, "Innovation Labs", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "mchen@innovationlabs.com", new DateTime(2025, 11, 18, 13, 50, 34, 185, DateTimeKind.Local).AddTicks(4450), "Michael Chen", "New prospect. Interested in our premium services. Follow up needed.", "(555) 333-4444", "Lead" },
                    { 4, "Startup Ventures", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "emily@startupventures.com", new DateTime(2025, 11, 27, 13, 50, 34, 185, DateTimeKind.Local).AddTicks(4460), "Emily Rodriguez", "Early stage company. Potential for growth partnership.", "(555) 444-5555", "Prospect" },
                    { 5, "Enterprise Systems", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "d.williams@enterprisesys.com", new DateTime(2025, 12, 1, 13, 50, 34, 185, DateTimeKind.Local).AddTicks(4460), "David Williams", "Long-term customer. Annual contract renewal coming up.", "(555) 555-6666", "Active" },
                    { 6, "Digital Solutions Group", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "lisa.anderson@digitalsolutions.com", new DateTime(2025, 9, 2, 13, 50, 34, 185, DateTimeKind.Local).AddTicks(4460), "Lisa Anderson", "Previous customer. No recent activity. May need re-engagement campaign.", "(555) 666-7777", "Inactive" },
                    { 7, "Mega Corp International", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "rtaylor@megacorp.com", new DateTime(2025, 11, 30, 13, 50, 34, 185, DateTimeKind.Local).AddTicks(4480), "Robert Taylor", "Large enterprise account. Dedicated account manager assigned.", "(555) 777-8888", "Active" }
                });

            migrationBuilder.InsertData(
                table: "CustomerReviews",
                columns: new[] { "Id", "CustomerId", "IsPublished", "Rating", "ReviewDate", "ReviewText", "ReviewerName", "Title" },
                values: new object[,]
                {
                    { 1, 1, true, 5, new DateTime(2025, 11, 2, 13, 50, 34, 185, DateTimeKind.Local).AddTicks(7390), "TechCorp Solutions has been working with this company for over a year. The service is outstanding, and the support team is always responsive. Highly recommend!", "John Smith", "Excellent Service and Support" },
                    { 2, 2, true, 5, new DateTime(2025, 10, 18, 13, 50, 34, 185, DateTimeKind.Local).AddTicks(7570), "We've been very satisfied with the products and services. The quality is consistently high, and delivery is always on time. Keep up the great work!", "Sarah Johnson", "Great Product Quality" },
                    { 3, 5, true, 4, new DateTime(2025, 11, 12, 13, 50, 34, 185, DateTimeKind.Local).AddTicks(7570), "Enterprise Systems has been a reliable partner for our business needs. The team understands our requirements and delivers accordingly. Very professional.", "David Williams", "Reliable Partner" },
                    { 4, 7, true, 5, new DateTime(2025, 11, 17, 13, 50, 34, 185, DateTimeKind.Local).AddTicks(7570), "Mega Corp International has been extremely pleased with the enterprise solution. The scalability and performance have exceeded our expectations. Excellent value for money.", "Robert Taylor", "Outstanding Enterprise Solution" },
                    { 5, 1, true, 5, new DateTime(2025, 11, 22, 13, 50, 34, 185, DateTimeKind.Local).AddTicks(7570), "What I appreciate most is the quick response time to any issues or questions. The customer service team is knowledgeable and helpful. Great experience overall.", "John Smith", "Quick Response Time" },
                    { 6, 2, true, 4, new DateTime(2025, 11, 7, 13, 50, 34, 185, DateTimeKind.Local).AddTicks(7570), "The pricing is competitive and the service quality is good. We've had a positive experience working with this company. Would recommend to others.", "Sarah Johnson", "Good Value" },
                    { 7, 5, true, 5, new DateTime(2025, 11, 27, 13, 50, 34, 185, DateTimeKind.Local).AddTicks(7580), "The team is very professional and easy to work with. They understand our business needs and provide tailored solutions. Very satisfied with the partnership.", "David Williams", "Professional Team" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CustomerReviews",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "CustomerReviews",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "CustomerReviews",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "CustomerReviews",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "CustomerReviews",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "CustomerReviews",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "CustomerReviews",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 7);
        }
    }
}
