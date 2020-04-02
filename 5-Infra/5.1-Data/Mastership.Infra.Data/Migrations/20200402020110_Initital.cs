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
                name: "BillingCustomer",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Enable = table.Column<bool>(nullable: false, defaultValue: true),
                    Deleted = table.Column<bool>(nullable: false, defaultValue: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    ChangeDate = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillingCustomer", x => x.Id);
                });

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
                    Longitude = table.Column<double>(nullable: false),
                    ForeignId = table.Column<string>(nullable: true),
                    BillingCustomerId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Company", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Company_BillingCustomer_BillingCustomerId",
                        column: x => x.BillingCustomerId,
                        principalTable: "BillingCustomer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Subsidiary",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Enable = table.Column<bool>(nullable: false, defaultValue: true),
                    Deleted = table.Column<bool>(nullable: false, defaultValue: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    ChangeDate = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    ForeignId = table.Column<string>(nullable: true),
                    CompanyId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subsidiary", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Subsidiary_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                    CPF = table.Column<string>(maxLength: 15, nullable: true),
                    Registration = table.Column<string>(nullable: true),
                    PIS = table.Column<string>(nullable: true),
                    AdmissionDate = table.Column<DateTime>(nullable: false),
                    Birthday = table.Column<DateTime>(nullable: false),
                    ForeignId = table.Column<string>(nullable: true),
                    SubsidiaryId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employee_Subsidiary_SubsidiaryId",
                        column: x => x.SubsidiaryId,
                        principalTable: "Subsidiary",
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

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Enable = table.Column<bool>(nullable: false, defaultValue: true),
                    Deleted = table.Column<bool>(nullable: false, defaultValue: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    ChangeDate = table.Column<DateTime>(nullable: false),
                    Username = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    EmployeeId = table.Column<Guid>(nullable: true),
                    BillingCustomerId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_BillingCustomer_BillingCustomerId",
                        column: x => x.BillingCustomerId,
                        principalTable: "BillingCustomer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_User_Employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "BillingCustomer",
                columns: new[] { "Id", "ChangeDate", "CreationDate", "Name" },
                values: new object[] { new Guid("8bd7a794-7dc8-41a2-be9a-e09ce16f7181"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "MConsult" });

            migrationBuilder.InsertData(
                table: "Company",
                columns: new[] { "Id", "Adress", "BillingCustomerId", "CNPJ", "ChangeDate", "CreationDate", "DomainName", "ForeignId", "Latitude", "Longitude", "Name", "RazaoSocial", "ZipCode" },
                values: new object[] { new Guid("90286f77-5cc9-4140-8cc5-e4e24510879e"), "V. DOM LUIS, 1200, TORRE 1, 21 ANDAR, SALA 2104 - Meireles, Fortaleza - CE", new Guid("8bd7a794-7dc8-41a2-be9a-e09ce16f7181"), "14.921.000/0001-39", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "mconsult", null, -3.7357805000000002, -38.490112000000003, "Mconsult", "M C Serviços de Tecnologia e Gestão LTDA", "60160-830" });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "BillingCustomerId", "ChangeDate", "CreationDate", "EmployeeId", "Password", "Username" },
                values: new object[] { new Guid("fe01e0a6-c73b-41b4-a963-0481b2476cb3"), new Guid("8bd7a794-7dc8-41a2-be9a-e09ce16f7181"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "mc123321", "mconsult" });

            migrationBuilder.InsertData(
                table: "Subsidiary",
                columns: new[] { "Id", "ChangeDate", "CompanyId", "CreationDate", "ForeignId", "Name" },
                values: new object[] { new Guid("a88c24f4-d6c9-4eba-8c86-67d515c3979f"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("90286f77-5cc9-4140-8cc5-e4e24510879e"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "MConsult" });

            migrationBuilder.InsertData(
                table: "Employee",
                columns: new[] { "Id", "AdmissionDate", "Birthday", "CPF", "ChangeDate", "CreationDate", "ForeignId", "FullName", "Name", "PIS", "Registration", "SubsidiaryId" },
                values: new object[] { new Guid("546d31b0-f719-4789-b5f2-7ff94afa72e8"), new DateTime(2020, 4, 1, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2020, 4, 1, 23, 1, 9, 905, DateTimeKind.Local).AddTicks(2133), "062.898.123-60", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, null, "87654321", new Guid("a88c24f4-d6c9-4eba-8c86-67d515c3979f") });

            migrationBuilder.CreateIndex(
                name: "IX_Company_BillingCustomerId",
                table: "Company",
                column: "BillingCustomerId");

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
                name: "IX_Employee_SubsidiaryId_Registration",
                table: "Employee",
                columns: new[] { "SubsidiaryId", "Registration" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PointTime_EmployeeId",
                table: "PointTime",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Subsidiary_CompanyId",
                table: "Subsidiary",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_User_BillingCustomerId",
                table: "User",
                column: "BillingCustomerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_EmployeeId",
                table: "User",
                column: "EmployeeId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PointTime");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Employee");

            migrationBuilder.DropTable(
                name: "Subsidiary");

            migrationBuilder.DropTable(
                name: "Company");

            migrationBuilder.DropTable(
                name: "BillingCustomer");
        }
    }
}
