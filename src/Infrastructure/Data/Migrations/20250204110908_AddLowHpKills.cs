using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QLStats.Infrastructure.Data.Migrations;

/// <inheritdoc />
public partial class AddLowHpKills : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<int>(
            name: "LowHpKillsPts",
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
            name: "LowHpKillsPts",
            schema: "QLSTATS",
            table: "MatchPlayerStats");
    }
}
