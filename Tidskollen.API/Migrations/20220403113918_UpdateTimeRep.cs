using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Tidskollen.API.Migrations
{
    public partial class UpdateTimeRep : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TimeReports",
                keyColumn: "ID",
                keyValue: 1);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "TimeReports",
                columns: new[] { "ID", "CheckIn", "CheckOut", "CheckStatus", "EmployeeId" },
                values: new object[] { 1, new DateTime(2022, 1, 7, 8, 5, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 1, 7, 17, 2, 0, 0, DateTimeKind.Unspecified), false, 1 });
        }
    }
}
