using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Mastership.Infra.Data.Migrations
{
    public partial class Initital : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Company",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Enable = table.Column<bool>(nullable: false, defaultValue: true),
                    Deleted = table.Column<bool>(nullable: false, defaultValue: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    ChangeDate = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    RazaoSocial = table.Column<string>(nullable: true),
                    DomainName = table.Column<string>(nullable: true),
                    CNPJ = table.Column<string>(maxLength: 18, nullable: true),
                    Adress = table.Column<string>(nullable: true),
                    ZipCode = table.Column<string>(nullable: true),
                    Latitude = table.Column<double>(nullable: false),
                    Longitude = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Company", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Employee",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Enable = table.Column<bool>(nullable: false, defaultValue: true),
                    Deleted = table.Column<bool>(nullable: false, defaultValue: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    ChangeDate = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    FullName = table.Column<string>(nullable: true),
                    CPF = table.Column<string>(maxLength: 9, nullable: true),
                    Registration = table.Column<string>(nullable: true),
                    PIS = table.Column<string>(nullable: true),
                    AdmissionDate = table.Column<DateTime>(nullable: false),
                    Birthday = table.Column<DateTime>(nullable: false),
                    CompanyId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employee_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PointTime",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Enable = table.Column<bool>(nullable: false, defaultValue: true),
                    Deleted = table.Column<bool>(nullable: false, defaultValue: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    ChangeDate = table.Column<DateTime>(nullable: false),
                    Day = table.Column<DateTime>(type: "date", nullable: false),
                    Hour = table.Column<TimeSpan>(type: "time", nullable: false),
                    Latitude = table.Column<decimal>(nullable: false),
                    Longitude = table.Column<decimal>(nullable: false),
                    IP = table.Column<string>(nullable: true),
                    UserHostName = table.Column<string>(nullable: true),
                    EmployeeId = table.Column<Guid>(nullable: false),
                    Sequential = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PointTime", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PointTime_Employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Company",
                columns: new[] { "Id", "Adress", "CNPJ", "ChangeDate", "CreationDate", "DomainName", "Latitude", "Longitude", "Name", "RazaoSocial", "ZipCode" },
                values: new object[] { new Guid("ab77270f-ff44-49fe-8fc4-afd178ae2b97"), " R. Pedro Borges, 30 - Centro, Fortaleza - CE", "10.347.407/0001-43", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "alldesk", -3.7280655999999999, -38.528249600000002, "MEIRELES, FREITAS E ALMEIDA SERVICOS DE TELEATENDIMENTO LTDA", "CALLDESK SOLUCOES EM CONTACT CENTER", "60030-200" });

            migrationBuilder.CreateIndex(
                name: "IX_Company_CNPJ",
                table: "Company",
                column: "CNPJ",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employee_CPF",
                table: "Employee",
                column: "CPF",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employee_CompanyId_Registration",
                table: "Employee",
                columns: new[] { "CompanyId", "Registration" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PointTime_EmployeeId",
                table: "PointTime",
                column: "EmployeeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PointTime");

            migrationBuilder.DropTable(
                name: "Employee");

            migrationBuilder.DropTable(
                name: "Company");
        }
    }
}
