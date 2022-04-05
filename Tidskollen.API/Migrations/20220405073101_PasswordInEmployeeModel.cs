using Microsoft.EntityFrameworkCore.Migrations;

namespace Tidskollen.API.Migrations
{
    public partial class PasswordInEmployeeModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Employees",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PasswordConfirmation",
                table: "Employees",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "PasswordConfirmation",
                table: "Employees");
        }
    }
}
