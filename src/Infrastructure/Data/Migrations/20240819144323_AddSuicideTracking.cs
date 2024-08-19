using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QLStats.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddSuicideTracking : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PtsPerSuicide",
                schema: "QLSTATS",
                table: "Seasons",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Suicides",
                schema: "QLSTATS",
                table: "MatchPlayerStats",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PtsPerSuicide",
                schema: "QLSTATS",
                table: "Seasons");

            migrationBuilder.DropColumn(
                name: "Suicides",
                schema: "QLSTATS",
                table: "MatchPlayerStats");
        }
    }
}
