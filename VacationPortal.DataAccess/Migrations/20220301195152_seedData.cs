using Microsoft.EntityFrameworkCore.Migrations;

namespace VacationPortal.DataAccess.Migrations
{
    public partial class seedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: -1);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "ModelStatus", "Name", "NormalizedName" },
                values: new object[] { 1, "242aae85-00d9-4faf-be66-dd0cbd7edc58", 0, "Admin", "ADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "ModelStatus", "Name", "NormalizedName" },
                values: new object[] { -1, "58efdec6-7052-44cc-9298-05754cc7a7da", 0, "Admin", "ADMIN" });
        }
    }
}
