using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Tidskollen.API.Migrations
{
    public partial class RequiredPasswordInModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: 3);

            migrationBuilder.AlterColumn<string>(
                name: "PasswordConfirmation",
                table: "Employees",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Employees",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PasswordConfirmation",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "EmployeeId", "DateOfBirth", "FirstName", "LastName", "Password", "PasswordConfirmation" },
                values: new object[] { 1, new DateTime(1990, 3, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Therese", "Brorsson", null, null });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "EmployeeId", "DateOfBirth", "FirstName", "LastName", "Password", "PasswordConfirmation" },
                values: new object[] { 2, new DateTime(1987, 2, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "Julia", "Karlsson", null, null });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "EmployeeId", "DateOfBirth", "FirstName", "LastName", "Password", "PasswordConfirmation" },
                values: new object[] { 3, new DateTime(1985, 12, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "Louisa", "Stark", null, null });
        }
    }
}
