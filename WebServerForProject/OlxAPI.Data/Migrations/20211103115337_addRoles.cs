using Microsoft.EntityFrameworkCore.Migrations;

namespace OlxAPI.Data.Migrations
{
    public partial class addRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "e29696c8-922e-48a6-b174-e6d4a45657a9", "448c7990-81b5-418c-85dd-0ea39ab00d3c", "Admin", null });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "e23fd5b5-7b22-4c0d-a0fc-ca0e24eac5bb", "58155875-9c74-41b4-8acd-7f9646d995bd", "User", null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e23fd5b5-7b22-4c0d-a0fc-ca0e24eac5bb");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e29696c8-922e-48a6-b174-e6d4a45657a9");
        }
    }
}
