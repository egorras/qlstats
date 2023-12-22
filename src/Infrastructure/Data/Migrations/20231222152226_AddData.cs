using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QLStats.Infrastructure.Migrations;

/// <inheritdoc />
public partial class AddData : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Matches",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                MatchGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Map = table.Column<string>(type: "nvarchar(max)", nullable: false),
                PlayedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                TeamBlueScore = table.Column<int>(type: "int", nullable: true),
                TeamRedScore = table.Column<int>(type: "int", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Matches", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Players",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                SteamId = table.Column<long>(type: "bigint", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Players", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "MatchPlayerStats",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                MatchId = table.Column<int>(type: "int", nullable: false),
                PlayerId = table.Column<int>(type: "int", nullable: false),
                Win = table.Column<bool>(type: "bit", nullable: false),
                Team = table.Column<int>(type: "int", nullable: true),
                TeamScore = table.Column<int>(type: "int", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_MatchPlayerStats", x => x.Id);
                table.ForeignKey(
                    name: "FK_MatchPlayerStats_Matches_MatchId",
                    column: x => x.MatchId,
                    principalTable: "Matches",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_MatchPlayerStats_Players_PlayerId",
                    column: x => x.PlayerId,
                    principalTable: "Players",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_MatchPlayerStats_MatchId",
            table: "MatchPlayerStats",
            column: "MatchId");

        migrationBuilder.CreateIndex(
            name: "IX_MatchPlayerStats_PlayerId",
            table: "MatchPlayerStats",
            column: "PlayerId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "MatchPlayerStats");

        migrationBuilder.DropTable(
            name: "Matches");

        migrationBuilder.DropTable(
            name: "Players");
    }
}
