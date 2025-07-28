using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JockeyInsight.WebApp.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "race_stats",
                columns: table => new
                {
                    id = table.Column<uint>(type: "int unsigned", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    race_name = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    race_date = table.Column<DateTime>(type: "date", nullable: false),
                    race_time = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    racecourse = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    race_distance = table.Column<int>(type: "int", nullable: false),
                    jockey = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    trainer = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    horse = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    finishing_position = table.Column<int>(type: "int", nullable: false),
                    distance_beaten = table.Column<decimal>(type: "decimal(6,2)", nullable: false),
                    time_beaten = table.Column<decimal>(type: "decimal(6,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_race_stats", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "notes",
                columns: table => new
                {
                    id = table.Column<uint>(type: "int unsigned", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    race_stat_id = table.Column<uint>(type: "int unsigned", nullable: false),
                    body = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_notes", x => x.id);
                    table.ForeignKey(
                        name: "FK_notes_race_stats_race_stat_id",
                        column: x => x.race_stat_id,
                        principalTable: "race_stats",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_notes_race_stat_id",
                table: "notes",
                column: "race_stat_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_race_stats_horse",
                table: "race_stats",
                column: "horse");

            migrationBuilder.CreateIndex(
                name: "IX_race_stats_jockey",
                table: "race_stats",
                column: "jockey");

            migrationBuilder.CreateIndex(
                name: "IX_race_stats_race_date",
                table: "race_stats",
                column: "race_date");

            migrationBuilder.CreateIndex(
                name: "IX_race_stats_racecourse",
                table: "race_stats",
                column: "racecourse");

            migrationBuilder.CreateIndex(
                name: "IX_race_stats_trainer",
                table: "race_stats",
                column: "trainer");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "notes");

            migrationBuilder.DropTable(
                name: "race_stats");
        }
    }
}
