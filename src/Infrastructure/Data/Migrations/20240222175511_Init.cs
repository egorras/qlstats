using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace QLStats.Infrastructure.Data.Migrations;

/// <inheritdoc />
public partial class Init : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.EnsureSchema(
            name: "QLSTATS");

        migrationBuilder.CreateTable(
            name: "Matches",
            schema: "QLSTATS",
            columns: table => new
            {
                Id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                MatchGuid = table.Column<Guid>(type: "uuid", nullable: false),
                Map = table.Column<string>(type: "text", nullable: false),
                PlayedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                TeamBlueScore = table.Column<int>(type: "integer", nullable: true),
                TeamRedScore = table.Column<int>(type: "integer", nullable: true),
                Aborted = table.Column<bool>(type: "boolean", nullable: false),
                CaptureLimit = table.Column<int>(type: "integer", nullable: false),
                ExitMsg = table.Column<string>(type: "text", nullable: false),
                Factory = table.Column<string>(type: "text", nullable: false),
                FactoryTitle = table.Column<string>(type: "text", nullable: false),
                FirstScorer = table.Column<string>(type: "text", nullable: false),
                FragLimit = table.Column<int>(type: "integer", nullable: false),
                GameLength = table.Column<int>(type: "integer", nullable: false),
                GameType = table.Column<string>(type: "text", nullable: false),
                Infected = table.Column<bool>(type: "boolean", nullable: false),
                Instagib = table.Column<bool>(type: "boolean", nullable: false),
                LastLeadChangeTime = table.Column<int>(type: "integer", nullable: false),
                LastScorer = table.Column<string>(type: "text", nullable: true),
                LastTeamscorer = table.Column<string>(type: "text", nullable: true),
                MercyLimit = table.Column<int>(type: "integer", nullable: false),
                Quadhog = table.Column<bool>(type: "boolean", nullable: false),
                Restarted = table.Column<bool>(type: "boolean", nullable: false),
                RoundLimit = table.Column<int>(type: "integer", nullable: false),
                ScoreLimit = table.Column<int>(type: "integer", nullable: false),
                ServerTitle = table.Column<string>(type: "text", nullable: false),
                TimeLimit = table.Column<int>(type: "integer", nullable: false),
                Training = table.Column<bool>(type: "boolean", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Matches", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Players",
            schema: "QLSTATS",
            columns: table => new
            {
                Id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                Name = table.Column<string>(type: "text", nullable: false),
                SteamId = table.Column<long>(type: "bigint", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Players", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Seasons",
            schema: "QLSTATS",
            columns: table => new
            {
                Id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                StartsAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                EndsAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                Name = table.Column<string>(type: "text", nullable: false),
                PtsForClanArenaRoundWin = table.Column<int>(type: "integer", nullable: false),
                PtsForClanArenaMatchWin = table.Column<int>(type: "integer", nullable: false),
                UseGameScore = table.Column<bool>(type: "boolean", nullable: false),
                DamageForOnePts = table.Column<decimal>(type: "numeric", nullable: true),
                PtsPerMedal = table.Column<int>(type: "integer", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Seasons", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "MatchPlayerStats",
            schema: "QLSTATS",
            columns: table => new
            {
                Id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                MatchId = table.Column<int>(type: "integer", nullable: false),
                PlayerId = table.Column<int>(type: "integer", nullable: false),
                Score = table.Column<int>(type: "integer", nullable: false),
                Win = table.Column<bool>(type: "boolean", nullable: false),
                Team = table.Column<int>(type: "integer", nullable: true),
                TeamScore = table.Column<int>(type: "integer", nullable: true),
                DamageDealt = table.Column<int>(type: "integer", nullable: false),
                DamageTaken = table.Column<int>(type: "integer", nullable: false),
                MedalsTotal = table.Column<int>(type: "integer", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_MatchPlayerStats", x => x.Id);
                table.ForeignKey(
                    name: "FK_MatchPlayerStats_Matches_MatchId",
                    column: x => x.MatchId,
                    principalSchema: "QLSTATS",
                    principalTable: "Matches",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_MatchPlayerStats_Players_PlayerId",
                    column: x => x.PlayerId,
                    principalSchema: "QLSTATS",
                    principalTable: "Players",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_MatchPlayerStats_MatchId",
            schema: "QLSTATS",
            table: "MatchPlayerStats",
            column: "MatchId");

        migrationBuilder.CreateIndex(
            name: "IX_MatchPlayerStats_PlayerId",
            schema: "QLSTATS",
            table: "MatchPlayerStats",
            column: "PlayerId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "MatchPlayerStats",
            schema: "QLSTATS");

        migrationBuilder.DropTable(
            name: "Seasons",
            schema: "QLSTATS");

        migrationBuilder.DropTable(
            name: "Matches",
            schema: "QLSTATS");

        migrationBuilder.DropTable(
            name: "Players",
            schema: "QLSTATS");
    }
}
