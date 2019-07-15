using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class Updateduserprofilemodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MarkAsDeleted",
                table: "Helper");

            migrationBuilder.DropColumn(
                name: "MarkAsDeleted",
                table: "Guarantor");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Employer");

            migrationBuilder.DropColumn(
                name: "MarkAsDeleted",
                table: "Employer");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "MarkAsDeleted",
                table: "Helper",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "MarkAsDeleted",
                table: "Guarantor",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Employer",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "MarkAsDeleted",
                table: "Employer",
                nullable: false,
                defaultValue: false);
        }
    }
}
