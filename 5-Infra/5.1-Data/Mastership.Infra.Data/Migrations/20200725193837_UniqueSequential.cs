using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Mastership.Infra.Data.Migrations
{
    public partial class UniqueSequential : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "SubsidiaryId",
                table: "PointTime",
                nullable: false,
                defaultValue: new Guid("a88c24f4-d6c9-4eba-8c86-67d515c3979f"));

            migrationBuilder.UpdateData(
                table: "Company",
                keyColumn: "Id",
                keyValue: new Guid("90286f77-5cc9-4140-8cc5-e4e24510879e"),
                column: "Enable",
                value: true);

            migrationBuilder.CreateIndex(
                name: "UN_Senquential",
                table: "PointTime",
                columns: new[] { "Sequential", "SubsidiaryId" },
                unique: true,
                filter: "((\"SubsidiaryId\" <> 'a88c24f4-d6c9-4eba-8c86-67d515c3979f'::uuid) or \"Sequential\">118802)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "UN_Senquential",
                table: "PointTime");

            migrationBuilder.DropColumn(
                name: "SubsidiaryId",
                table: "PointTime");

            migrationBuilder.UpdateData(
                table: "Company",
                keyColumn: "Id",
                keyValue: new Guid("90286f77-5cc9-4140-8cc5-e4e24510879e"),
                column: "Enable",
                value: true);
        }
    }
}
