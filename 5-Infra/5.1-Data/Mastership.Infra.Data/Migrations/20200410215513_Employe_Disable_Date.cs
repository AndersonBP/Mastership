using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Mastership.Infra.Data.Migrations
{
    public partial class Employe_Disable_Date : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DisabledDate",
                table: "Employee",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Employee",
                keyColumn: "Id",
                keyValue: new Guid("546d31b0-f719-4789-b5f2-7ff94afa72e8"),
                column: "AdmissionDate",
                value: new DateTime(2020, 4, 10, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Subsidiary",
                keyColumn: "Id",
                keyValue: new Guid("a88c24f4-d6c9-4eba-8c86-67d515c3979f"),
                column: "RazaoSocial",
                value: "MEIRELES, FREITAS E ALMEIDA SERVICOS DE TELEATENDIMENTO LTDA");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DisabledDate",
                table: "Employee");

            migrationBuilder.UpdateData(
                table: "Employee",
                keyColumn: "Id",
                keyValue: new Guid("546d31b0-f719-4789-b5f2-7ff94afa72e8"),
                column: "AdmissionDate",
                value: new DateTime(2020, 4, 8, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Subsidiary",
                keyColumn: "Id",
                keyValue: new Guid("a88c24f4-d6c9-4eba-8c86-67d515c3979f"),
                column: "RazaoSocial",
                value: "10.347.407/0001-4310.347.407/0001-43");
        }
    }
}
