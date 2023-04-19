using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Caarro.Migrations
{
    /// <inheritdoc />
    public partial class fueleconomy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "FuelEconomy",
                table: "Refueling",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FuelEconomy",
                table: "Refueling");
        }
    }
}
