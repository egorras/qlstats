using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QLStats.Infrastructure.Data.Migrations;

/// <inheritdoc />
public partial class UpdatePtsPerKill : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<decimal>(
            name: "PtsPerKill",
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
            name: "PtsPerKill",
            schema: "QLSTATS",
            table: "Seasons",
            type: "integer",
            nullable: false,
            oldClrType: typeof(decimal),
            oldType: "numeric");
    }
}
