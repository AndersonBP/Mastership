using System;
using System.Net;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Mastership.Infra.Data.Migrations
{
    public partial class CompanyIpRangeFilter : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CompanyIpRanges",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Enable = table.Column<bool>(nullable: false, defaultValue: true),
                    Deleted = table.Column<bool>(nullable: false, defaultValue: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    ChangeDate = table.Column<DateTime>(nullable: false),
                    Begin = table.Column<IPAddress>(type: "inet", nullable: false),
                    End = table.Column<IPAddress>(type: "inet", nullable: false),
                    CompanyId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyIpRanges", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyIpRanges_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CompanySettings",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Enable = table.Column<bool>(nullable: false, defaultValue: true),
                    Deleted = table.Column<bool>(nullable: false, defaultValue: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    ChangeDate = table.Column<DateTime>(nullable: false),
                    UseIpFilter = table.Column<bool>(nullable: false, defaultValueSql: "true"),
                    AllowMobile = table.Column<bool>(nullable: false, defaultValueSql: "false"),
                    CompanyId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanySettings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanySettings_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "Company",
                keyColumn: "Id",
                keyValue: new Guid("90286f77-5cc9-4140-8cc5-e4e24510879e"),
                column: "Enable",
                value: true);

            migrationBuilder.CreateIndex(
                name: "IX_CompanyIpRanges_CompanyId",
                table: "CompanyIpRanges",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanySettings_CompanyId",
                table: "CompanySettings",
                column: "CompanyId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompanyIpRanges");

            migrationBuilder.DropTable(
                name: "CompanySettings");

            migrationBuilder.UpdateData(
                table: "Company",
                keyColumn: "Id",
                keyValue: new Guid("90286f77-5cc9-4140-8cc5-e4e24510879e"),
                column: "Enable",
                value: true);
        }
    }
}
