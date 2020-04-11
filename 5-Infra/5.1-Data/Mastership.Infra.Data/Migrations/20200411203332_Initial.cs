using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Mastership.Infra.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                    Email = table.Column<string>(nullable: true),
                    UserType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BillingCustomer",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Enable = table.Column<bool>(nullable: false, defaultValue: true),
                    Deleted = table.Column<bool>(nullable: false, defaultValue: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    ChangeDate = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    UserId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillingCustomer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BillingCustomer_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                    Image = table.Column<string>(nullable: true),
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
                    RazaoSocial = table.Column<string>(nullable: true),
                    CNPJ = table.Column<string>(nullable: true),
                    CEI = table.Column<string>(nullable: true),
                    REP = table.Column<string>(maxLength: 17, nullable: true),
                    Adress = table.Column<string>(nullable: true),
                    ZipCode = table.Column<string>(nullable: true),
                    Latitude = table.Column<double>(nullable: false),
                    Longitude = table.Column<double>(nullable: false),
                    UserId = table.Column<Guid>(nullable: true),
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
                    table.ForeignKey(
                        name: "FK_Subsidiary_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
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
                    Name = table.Column<string>(nullable: false),
                    FullName = table.Column<string>(nullable: false),
                    CPF = table.Column<string>(maxLength: 15, nullable: true),
                    Registration = table.Column<string>(nullable: true),
                    PIS = table.Column<string>(nullable: true),
                    RG = table.Column<string>(nullable: true),
                    AdmissionDate = table.Column<DateTime>(type: "date", nullable: false),
                    Birthday = table.Column<DateTime>(type: "date", nullable: false),
                    Email = table.Column<string>(nullable: true),
                    ForeignId = table.Column<string>(nullable: true),
                    DisabledDate = table.Column<DateTime>(nullable: true),
                    UserId = table.Column<Guid>(nullable: true),
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
                    table.ForeignKey(
                        name: "FK_Employee_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
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
                    DateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
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
                table: "BillingCustomer",
                columns: new[] { "Id", "ChangeDate", "CreationDate", "Name", "UserId" },
                values: new object[] { new Guid("8bd7a794-7dc8-41a2-be9a-e09ce16f7181"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Alldesk", null });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "ChangeDate", "CreationDate", "Email", "Password", "UserType", "Username" },
                values: new object[] { new Guid("fe01e0a6-c73b-41b4-a963-0481b2476cb3"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "621ae3d14b1c50f8227c7901ade7982a", 1, "alldesk" });

            migrationBuilder.InsertData(
                table: "Company",
                columns: new[] { "Id", "Adress", "BillingCustomerId", "CNPJ", "ChangeDate", "CreationDate", "DomainName", "Enable", "ForeignId", "Image", "Latitude", "Longitude", "Name", "RazaoSocial", "ZipCode" },
                values: new object[] { new Guid("90286f77-5cc9-4140-8cc5-e4e24510879e"), "RUA PEDRO BORGES , 30, SALAS 101 A 110 1 ANDAR", new Guid("8bd7a794-7dc8-41a2-be9a-e09ce16f7181"), "10.347.407/0001-43", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 4, 11, 17, 33, 31, 922, DateTimeKind.Local).AddTicks(9731), "alldesk", true, null, "11042020173331923logo-alldesk.png", -3.7357805000000002, -38.490112000000003, "AllDesk", "MEIRELES, FREITAS E ALMEIDA SERVICOS DE TELEATENDIMENTO LTDA", "60055-110" });

            migrationBuilder.InsertData(
                table: "Subsidiary",
                columns: new[] { "Id", "Adress", "CEI", "CNPJ", "ChangeDate", "CompanyId", "CreationDate", "ForeignId", "Latitude", "Longitude", "Name", "REP", "RazaoSocial", "UserId", "ZipCode" },
                values: new object[] { new Guid("a88c24f4-d6c9-4eba-8c86-67d515c3979f"), "RUA PEDRO BORGES , 30, SALAS 101 A 110 1 ANDAR", null, "10.347.407/0001-43", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("90286f77-5cc9-4140-8cc5-e4e24510879e"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, -3.7357805000000002, -38.490112000000003, "Alldesk", "00000000000000000", "MEIRELES, FREITAS E ALMEIDA SERVICOS DE TELEATENDIMENTO LTDA", new Guid("fe01e0a6-c73b-41b4-a963-0481b2476cb3"), "60055-110" });

            migrationBuilder.CreateIndex(
                name: "IX_BillingCustomer_UserId",
                table: "BillingCustomer",
                column: "UserId");

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
                name: "IX_Company_DomainName",
                table: "Company",
                column: "DomainName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employee_UserId",
                table: "Employee",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_SubsidiaryId_CPF",
                table: "Employee",
                columns: new[] { "SubsidiaryId", "CPF" },
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
                name: "IX_Subsidiary_REP",
                table: "Subsidiary",
                column: "REP",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Subsidiary_UserId",
                table: "Subsidiary",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PointTime");

            migrationBuilder.DropTable(
                name: "Employee");

            migrationBuilder.DropTable(
                name: "Subsidiary");

            migrationBuilder.DropTable(
                name: "Company");

            migrationBuilder.DropTable(
                name: "BillingCustomer");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
