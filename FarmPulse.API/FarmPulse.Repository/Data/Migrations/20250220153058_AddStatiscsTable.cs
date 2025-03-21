using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FarmPulse.Repository.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddStatiscsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Statistic",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AverageWeight = table.Column<double>(type: "float", nullable: false),
                    MortalityRate = table.Column<double>(type: "float", nullable: false),
                    SurvivalRate = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statistic", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Statistic");
        }
    }
}
