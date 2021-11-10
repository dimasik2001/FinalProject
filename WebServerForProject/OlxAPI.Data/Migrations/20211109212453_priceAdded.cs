using Microsoft.EntityFrameworkCore.Migrations;

namespace OlxAPI.Data.Migrations
{
    public partial class priceAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4c6d8398-466a-45b5-b7cf-9cff37ae030a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b34a5c68-0d45-4070-a71a-3722ea99a019");

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Ads",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "67b4e685-4d2c-4c2f-92fb-c410cdf088d4", "3558c415-e9a9-49ba-967f-d0b83e8b2385", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "1b86cd75-c208-44e8-85f2-6526a77a0b0f", "f0da3db2-3653-4eeb-9678-afcafd7d11ba", "User", "USER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1b86cd75-c208-44e8-85f2-6526a77a0b0f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "67b4e685-4d2c-4c2f-92fb-c410cdf088d4");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Ads");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "4c6d8398-466a-45b5-b7cf-9cff37ae030a", "7d6d2537-91b1-4658-b1af-1a2667dd3152", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "b34a5c68-0d45-4070-a71a-3722ea99a019", "c2facc5a-e443-4d04-9dd4-2f0db2b19a97", "User", "USER" });
        }
    }
}
