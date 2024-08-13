using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QLStats.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddPtsPerKill : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PtsPerKill",
                schema: "QLSTATS",
                table: "Seasons",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PtsPerKill",
                schema: "QLSTATS",
                table: "Seasons");
        }
    }
}
