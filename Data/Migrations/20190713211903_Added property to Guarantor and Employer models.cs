using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class AddedpropertytoGuarantorandEmployermodels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Guarantor",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Employer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Guarantor_UserId",
                table: "Guarantor",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Employer_UserId",
                table: "Employer",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employer_AspNetUsers_UserId",
                table: "Employer",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Guarantor_AspNetUsers_UserId",
                table: "Guarantor",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employer_AspNetUsers_UserId",
                table: "Employer");

            migrationBuilder.DropForeignKey(
                name: "FK_Guarantor_AspNetUsers_UserId",
                table: "Guarantor");

            migrationBuilder.DropIndex(
                name: "IX_Guarantor_UserId",
                table: "Guarantor");

            migrationBuilder.DropIndex(
                name: "IX_Employer_UserId",
                table: "Employer");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Guarantor");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Employer");
        }
    }
}
