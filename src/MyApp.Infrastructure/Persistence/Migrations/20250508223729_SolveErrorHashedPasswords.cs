using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyApp.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class SolveErrorHashedPasswords : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PasswordHash",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                columns: new[] { "Password", "PasswordHash" },
                values: new object[] { null, "$2a$11$6i/nZg0pwd.3JygOZbOkL.O7B9l5z5lTv2oya0MYeCOT.olksAHwi" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                columns: new[] { "Password", "PasswordHash" },
                values: new object[] { null, "$2a$11$anPFxflI.0ogrw8Ah5z8wuY.10MOuiI.KUFlm2CEuKHTWmozkvHdK" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PasswordHash",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                columns: new[] { "Password", "PasswordHash" },
                values: new object[] { "$2a$11$6i/nZg0pwd.3JygOZbOkL.O7B9l5z5lTv2oya0MYeCOT.olksAHwi", null });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                columns: new[] { "Password", "PasswordHash" },
                values: new object[] { "$2a$11$anPFxflI.0ogrw8Ah5z8wuY.10MOuiI.KUFlm2CEuKHTWmozkvHdK", null });
        }
    }
}
