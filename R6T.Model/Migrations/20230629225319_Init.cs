using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace R6T.Model.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MatchTypes",
                columns: table => new
                {
                    MatchTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MatchTypeName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchTypes", x => x.MatchTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    PlayerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PlayerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Alias = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RankUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SortOrder = table.Column<int>(type: "int", nullable: true),
                    LatestAlias = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.PlayerId);
                });

            migrationBuilder.CreateTable(
                name: "GameStats",
                columns: table => new
                {
                    GameStatId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PlayerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    MatchTypeId = table.Column<int>(type: "int", nullable: true),
                    PlayerLevel = table.Column<int>(type: "int", nullable: true),
                    MatchesPlayed = table.Column<int>(type: "int", nullable: true),
                    Wins = table.Column<int>(type: "int", nullable: true),
                    Losses = table.Column<int>(type: "int", nullable: true),
                    Kills = table.Column<int>(type: "int", nullable: true),
                    Deaths = table.Column<int>(type: "int", nullable: true),
                    Headshots = table.Column<int>(type: "int", nullable: true),
                    MeleeKills = table.Column<int>(type: "int", nullable: true),
                    BlindKills = table.Column<int>(type: "int", nullable: true),
                    KD = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TimePlayed = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TotalXp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    WinPercent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HeadshotPercent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KillPerMatch = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    KillPerMin = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    RankUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MMR = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameStats", x => x.GameStatId);
                    table.ForeignKey(
                        name: "FK_GameStats_MatchTypes_MatchTypeId",
                        column: x => x.MatchTypeId,
                        principalTable: "MatchTypes",
                        principalColumn: "MatchTypeId");
                    table.ForeignKey(
                        name: "FK_GameStats_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "PlayerId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_GameStats_MatchTypeId",
                table: "GameStats",
                column: "MatchTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_GameStats_PlayerId",
                table: "GameStats",
                column: "PlayerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GameStats");

            migrationBuilder.DropTable(
                name: "MatchTypes");

            migrationBuilder.DropTable(
                name: "Players");
        }
    }
}
