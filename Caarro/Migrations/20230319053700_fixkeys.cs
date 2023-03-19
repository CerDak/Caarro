using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Caarro.Migrations
{
    /// <inheritdoc />
    public partial class fixkeys : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expenses_Vehicles_VehicleId",
                table: "Expenses");

            migrationBuilder.DropForeignKey(
                name: "FK_Income_Vehicles_VehicleId",
                table: "Income");

            migrationBuilder.DropForeignKey(
                name: "FK_Refueling_Vehicles_VehicleId",
                table: "Refueling");

            migrationBuilder.DropForeignKey(
                name: "FK_Reminders_Vehicles_VehicleId",
                table: "Reminders");

            migrationBuilder.DropForeignKey(
                name: "FK_Services_Vehicles_VehicleId",
                table: "Services");

            migrationBuilder.AlterColumn<int>(
                name: "VehicleId",
                table: "Services",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "Amount",
                table: "Services",
                type: "REAL",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "REAL");

            migrationBuilder.AlterColumn<int>(
                name: "VehicleId",
                table: "Reminders",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ReminderPeriodDistance",
                table: "Reminders",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(uint),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "VehicleId",
                table: "Refueling",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "VehicleId",
                table: "Income",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "VehicleId",
                table: "Expenses",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Expenses_Vehicles_VehicleId",
                table: "Expenses",
                column: "VehicleId",
                principalTable: "Vehicles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Income_Vehicles_VehicleId",
                table: "Income",
                column: "VehicleId",
                principalTable: "Vehicles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Refueling_Vehicles_VehicleId",
                table: "Refueling",
                column: "VehicleId",
                principalTable: "Vehicles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reminders_Vehicles_VehicleId",
                table: "Reminders",
                column: "VehicleId",
                principalTable: "Vehicles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Services_Vehicles_VehicleId",
                table: "Services",
                column: "VehicleId",
                principalTable: "Vehicles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expenses_Vehicles_VehicleId",
                table: "Expenses");

            migrationBuilder.DropForeignKey(
                name: "FK_Income_Vehicles_VehicleId",
                table: "Income");

            migrationBuilder.DropForeignKey(
                name: "FK_Refueling_Vehicles_VehicleId",
                table: "Refueling");

            migrationBuilder.DropForeignKey(
                name: "FK_Reminders_Vehicles_VehicleId",
                table: "Reminders");

            migrationBuilder.DropForeignKey(
                name: "FK_Services_Vehicles_VehicleId",
                table: "Services");

            migrationBuilder.AlterColumn<int>(
                name: "VehicleId",
                table: "Services",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<double>(
                name: "Amount",
                table: "Services",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "REAL",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "VehicleId",
                table: "Reminders",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<uint>(
                name: "ReminderPeriodDistance",
                table: "Reminders",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0u,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "VehicleId",
                table: "Refueling",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "VehicleId",
                table: "Income",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "VehicleId",
                table: "Expenses",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_Expenses_Vehicles_VehicleId",
                table: "Expenses",
                column: "VehicleId",
                principalTable: "Vehicles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Income_Vehicles_VehicleId",
                table: "Income",
                column: "VehicleId",
                principalTable: "Vehicles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Refueling_Vehicles_VehicleId",
                table: "Refueling",
                column: "VehicleId",
                principalTable: "Vehicles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reminders_Vehicles_VehicleId",
                table: "Reminders",
                column: "VehicleId",
                principalTable: "Vehicles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Services_Vehicles_VehicleId",
                table: "Services",
                column: "VehicleId",
                principalTable: "Vehicles",
                principalColumn: "Id");
        }
    }
}
