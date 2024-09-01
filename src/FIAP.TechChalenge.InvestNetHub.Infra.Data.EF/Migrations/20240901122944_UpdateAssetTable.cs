using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FIAP.TechChalenge.InvestNetHub.Infra.Data.EF.Migrations
{
    public partial class UpdateAssetTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Assets",
                type: "decimal(65,30)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Assets",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "Assets");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Assets");
        }
    }
}
