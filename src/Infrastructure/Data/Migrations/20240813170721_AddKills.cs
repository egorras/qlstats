using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QLStats.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddKills : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Kills",
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
                name: "Kills",
                schema: "QLSTATS",
                table: "MatchPlayerStats");
        }
    }
}
