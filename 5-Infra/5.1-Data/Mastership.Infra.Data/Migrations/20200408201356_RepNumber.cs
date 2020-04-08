using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Mastership.Infra.Data.Migrations
{
    public partial class RepNumber : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "REP",
                table: "Subsidiary",
                maxLength: 17,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Employee",
                keyColumn: "Id",
                keyValue: new Guid("546d31b0-f719-4789-b5f2-7ff94afa72e8"),
                column: "AdmissionDate",
                value: new DateTime(2020, 4, 8, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.CreateIndex(
                name: "IX_Subsidiary_REP",
                table: "Subsidiary",
                column: "REP",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Subsidiary_REP",
                table: "Subsidiary");

            migrationBuilder.DropColumn(
                name: "REP",
                table: "Subsidiary");

            migrationBuilder.UpdateData(
                table: "Employee",
                keyColumn: "Id",
                keyValue: new Guid("546d31b0-f719-4789-b5f2-7ff94afa72e8"),
                column: "AdmissionDate",
                value: new DateTime(2020, 4, 7, 0, 0, 0, 0, DateTimeKind.Local));
        }
    }
}
