using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QLStats.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddMatchData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Aborted",
                table: "Matches",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "CaptureLimit",
                table: "Matches",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ExitMsg",
                table: "Matches",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Factory",
                table: "Matches",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FactoryTitle",
                table: "Matches",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FirstScorer",
                table: "Matches",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "FragLimit",
                table: "Matches",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "GameLength",
                table: "Matches",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "GameType",
                table: "Matches",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "Infected",
                table: "Matches",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Instagib",
                table: "Matches",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "LastLeadChangeTime",
                table: "Matches",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "LastScorer",
                table: "Matches",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastTeamScorer",
                table: "Matches",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "MercyLimit",
                table: "Matches",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "Quadhog",
                table: "Matches",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Restarted",
                table: "Matches",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "RoundLimit",
                table: "Matches",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ScoreLimit",
                table: "Matches",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ServerTitle",
                table: "Matches",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Matches",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "TimeLimit",
                table: "Matches",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "Training",
                table: "Matches",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Aborted",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "CaptureLimit",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "ExitMsg",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "Factory",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "FactoryTitle",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "FirstScorer",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "FragLimit",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "GameLength",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "GameType",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "Infected",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "Instagib",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "LastLeadChangeTime",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "LastScorer",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "LastTeamScorer",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "MercyLimit",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "Quadhog",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "Restarted",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "RoundLimit",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "ScoreLimit",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "ServerTitle",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "TimeLimit",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "Training",
                table: "Matches");
        }
    }
}
