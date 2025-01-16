using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QLStats.Infrastructure.Data.Migrations;

/// <inheritdoc />
public partial class UpdateData : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<decimal>(
            name: "PtsForClanArenaRoundWin",
            schema: "QLSTATS",
            table: "Seasons",
            type: "numeric",
            nullable: false,
            oldClrType: typeof(int),
            oldType: "integer");

        migrationBuilder.AlterColumn<decimal>(
            name: "PtsForClanArenaMatchWin",
            schema: "QLSTATS",
            table: "Seasons",
            type: "numeric",
            nullable: false,
            oldClrType: typeof(int),
            oldType: "integer");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<int>(
            name: "PtsForClanArenaRoundWin",
            schema: "QLSTATS",
            table: "Seasons",
            type: "integer",
            nullable: false,
            oldClrType: typeof(decimal),
            oldType: "numeric");

        migrationBuilder.AlterColumn<int>(
            name: "PtsForClanArenaMatchWin",
            schema: "QLSTATS",
            table: "Seasons",
            type: "integer",
            nullable: false,
            oldClrType: typeof(decimal),
            oldType: "numeric");
    }
}
