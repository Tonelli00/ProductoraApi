using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UserSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "EVENT",
                keyColumn: "Id",
                keyValue: 1,
                column: "EventDate",
                value: new DateTime(2026, 5, 20, 23, 51, 47, 806, DateTimeKind.Local).AddTicks(5661));

            migrationBuilder.InsertData(
                table: "USER",
                columns: new[] { "Id", "Email", "Name", "PasswordHash" },
                values: new object[] { 1, "Proyecto2026@gmail.com", "Proyecto", "$2a$11$aS/QSa9iaBs4pDo.ciZMpOfKepKZt0WkhwjOgjWxGXWY26YY9jcsu" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "USER",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.UpdateData(
                table: "EVENT",
                keyColumn: "Id",
                keyValue: 1,
                column: "EventDate",
                value: new DateTime(2026, 5, 20, 23, 29, 13, 94, DateTimeKind.Local).AddTicks(4870));
        }
    }
}
