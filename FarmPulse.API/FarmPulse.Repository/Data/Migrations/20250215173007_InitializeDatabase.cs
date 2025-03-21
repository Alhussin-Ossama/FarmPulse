using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FarmPulse.Repository.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitializeDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Chicken",
                columns: table => new
                {
                    ChickenId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RFID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CurrentWeight = table.Column<double>(type: "float", nullable: false),
                    ActivityStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chicken", x => x.ChickenId);
                });

            migrationBuilder.CreateTable(
                name: "Notification",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsRead = table.Column<bool>(type: "bit", nullable: false),
                    ChickenId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notification", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notification_Chicken_ChickenId",
                        column: x => x.ChickenId,
                        principalTable: "Chicken",
                        principalColumn: "ChickenId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Weight",
                columns: table => new
                {
                    WeightID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EntryWeight = table.Column<double>(type: "float", nullable: false),
                    ExitWeight = table.Column<double>(type: "float", nullable: false),
                    EntryTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExitTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ChickenId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Weight", x => x.WeightID);
                    table.ForeignKey(
                        name: "FK_Weight_Chicken_ChickenId",
                        column: x => x.ChickenId,
                        principalTable: "Chicken",
                        principalColumn: "ChickenId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Notification_ChickenId",
                table: "Notification",
                column: "ChickenId");

            migrationBuilder.CreateIndex(
                name: "IX_Weight_ChickenId",
                table: "Weight",
                column: "ChickenId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Notification");

            migrationBuilder.DropTable(
                name: "Weight");

            migrationBuilder.DropTable(
                name: "Chicken");
        }
    }
}
