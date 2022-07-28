using Microsoft.EntityFrameworkCore.Migrations;

namespace OlxAPI.Data.Migrations
{
    public partial class roleUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e23fd5b5-7b22-4c0d-a0fc-ca0e24eac5bb");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e29696c8-922e-48a6-b174-e6d4a45657a9");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "4c6d8398-466a-45b5-b7cf-9cff37ae030a", "7d6d2537-91b1-4658-b1af-1a2667dd3152", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "b34a5c68-0d45-4070-a71a-3722ea99a019", "c2facc5a-e443-4d04-9dd4-2f0db2b19a97", "User", "USER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4c6d8398-466a-45b5-b7cf-9cff37ae030a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b34a5c68-0d45-4070-a71a-3722ea99a019");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "e29696c8-922e-48a6-b174-e6d4a45657a9", "448c7990-81b5-418c-85dd-0ea39ab00d3c", "Admin", null });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "e23fd5b5-7b22-4c0d-a0fc-ca0e24eac5bb", "58155875-9c74-41b4-8acd-7f9646d995bd", "User", null });
        }
    }
}
