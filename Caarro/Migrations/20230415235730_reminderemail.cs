using System;

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Caarro.Migrations
{
    /// <inheritdoc />
    public partial class reminderemail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ContactEmail",
                table: "Reminders",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "JobId",
                table: "Reminders",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContactEmail",
                table: "Reminders");

            migrationBuilder.DropColumn(
                name: "JobId",
                table: "Reminders");
        }
    }
}
