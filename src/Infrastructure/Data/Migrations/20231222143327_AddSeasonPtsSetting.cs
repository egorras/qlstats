using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QLStats.Infrastructure.Migrations;

/// <inheritdoc />
public partial class AddSeasonPtsSetting : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<int>(
            name: "PtsForClanArenaMatchWin",
            table: "Seasons",
            type: "int",
            nullable: false,
            defaultValue: 0);

        migrationBuilder.AddColumn<int>(
            name: "PtsForClanArenaRoundWin",
            table: "Seasons",
            type: "int",
            nullable: false,
            defaultValue: 0);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "PtsForClanArenaMatchWin",
            table: "Seasons");

        migrationBuilder.DropColumn(
            name: "PtsForClanArenaRoundWin",
            table: "Seasons");
    }
}
