using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QLStats.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CleanupMatchData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Matches");

            migrationBuilder.RenameColumn(
                name: "LastTeamScorer",
                table: "Matches",
                newName: "LastTeamscorer");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LastTeamscorer",
                table: "Matches",
                newName: "LastTeamScorer");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Matches",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
