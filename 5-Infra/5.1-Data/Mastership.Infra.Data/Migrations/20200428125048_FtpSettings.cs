using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Mastership.Infra.Data.Migrations
{
    public partial class FtpSettings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "AFDScheduled",
                table: "CompanySettings",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "FTPHost",
                table: "CompanySettings",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FTPPass",
                table: "CompanySettings",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FTPPath",
                table: "CompanySettings",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FTPUser",
                table: "CompanySettings",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Company",
                keyColumn: "Id",
                keyValue: new Guid("90286f77-5cc9-4140-8cc5-e4e24510879e"),
                column: "Enable",
                value: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AFDScheduled",
                table: "CompanySettings");

            migrationBuilder.DropColumn(
                name: "FTPHost",
                table: "CompanySettings");

            migrationBuilder.DropColumn(
                name: "FTPPass",
                table: "CompanySettings");

            migrationBuilder.DropColumn(
                name: "FTPPath",
                table: "CompanySettings");

            migrationBuilder.DropColumn(
                name: "FTPUser",
                table: "CompanySettings");

            migrationBuilder.UpdateData(
                table: "Company",
                keyColumn: "Id",
                keyValue: new Guid("90286f77-5cc9-4140-8cc5-e4e24510879e"),
                column: "Enable",
                value: true);
        }
    }
}
