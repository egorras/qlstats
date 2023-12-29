using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QLStats.Infrastructure.Migrations;

/// <inheritdoc />
public partial class AddDamagePerPts : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<decimal>(
            name: "DamageForOnePts",
            table: "Seasons",
            type: "decimal(18,2)",
            nullable: true);

        migrationBuilder.AddColumn<int>(
            name: "DamageDealt",
            table: "MatchPlayerStats",
            type: "int",
            nullable: false,
            defaultValue: 0);

        migrationBuilder.AddColumn<int>(
            name: "DamageTaken",
            table: "MatchPlayerStats",
            type: "int",
            nullable: false,
            defaultValue: 0);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "DamageForOnePts",
            table: "Seasons");

        migrationBuilder.DropColumn(
            name: "DamageDealt",
            table: "MatchPlayerStats");

        migrationBuilder.DropColumn(
            name: "DamageTaken",
            table: "MatchPlayerStats");
    }
}
