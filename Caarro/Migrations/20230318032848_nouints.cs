using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Caarro.Migrations
{
    /// <inheritdoc />
    public partial class nouints : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Year",
                table: "Vehicles",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(uint),
                oldType: "INTEGER");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<uint>(
                name: "Year",
                table: "Vehicles",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0u,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);
        }
    }
}
