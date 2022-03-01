using Microsoft.EntityFrameworkCore.Migrations;

namespace VacationPortal.DataAccess.Migrations
{
    public partial class AddModelStatusColumnToTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ModelStatus",
                table: "VacationInfos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ModelStatus",
                table: "VacationApplications",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ModelStatus",
                table: "Positions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ModelStatus",
                table: "Departments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ModelStatus",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ModelStatus",
                table: "AspNetRoles",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ModelStatus",
                table: "VacationInfos");

            migrationBuilder.DropColumn(
                name: "ModelStatus",
                table: "VacationApplications");

            migrationBuilder.DropColumn(
                name: "ModelStatus",
                table: "Positions");

            migrationBuilder.DropColumn(
                name: "ModelStatus",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "ModelStatus",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ModelStatus",
                table: "AspNetRoles");
        }
    }
}
