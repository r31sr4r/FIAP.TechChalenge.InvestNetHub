using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FIAP.TechChalenge.InvestNetHub.Infra.Data.EF.Migrations
{
    public partial class AddAnalysisFieldsToUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "AnalysisDate",
                table: "Users",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AnalysisStatus",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "InvestmentPreferences",
                table: "Users",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "RiskLevel",
                table: "Users",
                type: "int",
                nullable: true,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AnalysisDate",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "AnalysisStatus",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "InvestmentPreferences",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "RiskLevel",
                table: "Users");
        }
    }
}
