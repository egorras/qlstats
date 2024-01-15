using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QLStats.Infrastructure.Migrations;

/// <inheritdoc />
public partial class AddMedals : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<int>(
            name: "PtsPerMedal",
            table: "Seasons",
            type: "int",
            nullable: false,
            defaultValue: 0);

        migrationBuilder.AddColumn<int>(
            name: "MedalsTotal",
            table: "MatchPlayerStats",
            type: "int",
            nullable: false,
            defaultValue: 0);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "PtsPerMedal",
            table: "Seasons");

        migrationBuilder.DropColumn(
            name: "MedalsTotal",
            table: "MatchPlayerStats");
    }
}
