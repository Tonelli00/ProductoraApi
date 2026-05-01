using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class NuevaMigracion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EVENT",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(100)", nullable: false),
                    EventDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Venue = table.Column<string>(type: "varchar(150)", nullable: false),
                    Status = table.Column<string>(type: "varchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EVENT", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "USER",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(100)", nullable: false),
                    Email = table.Column<string>(type: "varchar(150)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USER", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SECTOR",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "varchar(100)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Capacity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SECTOR", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SECTOR_EVENT_EventId",
                        column: x => x.EventId,
                        principalTable: "EVENT",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AUDIT_LOG",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    Action = table.Column<string>(type: "varchar(50)", nullable: false),
                    EntityType = table.Column<string>(type: "varchar(50)", nullable: false),
                    EntityId = table.Column<string>(type: "varchar(50)", nullable: false),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AUDIT_LOG", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AUDIT_LOG_USER_UserId",
                        column: x => x.UserId,
                        principalTable: "USER",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "SEAT",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    SectorId = table.Column<int>(type: "int", nullable: false),
                    RowIdentifier = table.Column<string>(type: "varchar(10)", nullable: false),
                    SeatNumber = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<string>(type: "varchar(50)", nullable: false),
                    Version = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SEAT", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SEAT_SECTOR_SectorId",
                        column: x => x.SectorId,
                        principalTable: "SECTOR",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RESERVATION",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    SeatId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<string>(type: "varchar(20)", nullable: false),
                    ReservedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpiresAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RESERVATION", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RESERVATION_SEAT_SeatId",
                        column: x => x.SeatId,
                        principalTable: "SEAT",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RESERVATION_USER_UserId",
                        column: x => x.UserId,
                        principalTable: "USER",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "EVENT",
                columns: new[] { "Id", "EventDate", "Name", "Status", "Venue" },
                values: new object[] { 1, new DateTime(2026, 5, 20, 20, 55, 48, 815, DateTimeKind.Local).AddTicks(5727), "Gran evento", "Activo", "Estadio A" });

            migrationBuilder.InsertData(
                table: "SECTOR",
                columns: new[] { "Id", "Capacity", "EventId", "Name", "Price" },
                values: new object[,]
                {
                    { 1, 50, 1, "Sector A", 2000m },
                    { 2, 50, 1, "Sector B", 2500m }
                });

            migrationBuilder.InsertData(
                table: "SEAT",
                columns: new[] { "Id", "RowIdentifier", "SeatNumber", "SectorId", "Status", "Version" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-000000000001"), "Sector 1", 1, 1, "Available", 1 },
                    { new Guid("11111111-1111-1111-1111-000000000002"), "Sector 1", 2, 1, "Available", 1 },
                    { new Guid("11111111-1111-1111-1111-000000000003"), "Sector 1", 3, 1, "Available", 1 },
                    { new Guid("11111111-1111-1111-1111-000000000004"), "Sector 1", 4, 1, "Available", 1 },
                    { new Guid("11111111-1111-1111-1111-000000000005"), "Sector 1", 5, 1, "Available", 1 },
                    { new Guid("11111111-1111-1111-1111-000000000006"), "Sector 1", 6, 1, "Available", 1 },
                    { new Guid("11111111-1111-1111-1111-000000000007"), "Sector 1", 7, 1, "Available", 1 },
                    { new Guid("11111111-1111-1111-1111-000000000008"), "Sector 1", 8, 1, "Available", 1 },
                    { new Guid("11111111-1111-1111-1111-000000000009"), "Sector 1", 9, 1, "Available", 1 },
                    { new Guid("11111111-1111-1111-1111-000000000010"), "Sector 1", 10, 1, "Available", 1 },
                    { new Guid("11111111-1111-1111-1111-000000000011"), "Sector 1", 11, 1, "Available", 1 },
                    { new Guid("11111111-1111-1111-1111-000000000012"), "Sector 1", 12, 1, "Available", 1 },
                    { new Guid("11111111-1111-1111-1111-000000000013"), "Sector 1", 13, 1, "Available", 1 },
                    { new Guid("11111111-1111-1111-1111-000000000014"), "Sector 1", 14, 1, "Available", 1 },
                    { new Guid("11111111-1111-1111-1111-000000000015"), "Sector 1", 15, 1, "Available", 1 },
                    { new Guid("11111111-1111-1111-1111-000000000016"), "Sector 1", 16, 1, "Available", 1 },
                    { new Guid("11111111-1111-1111-1111-000000000017"), "Sector 1", 17, 1, "Available", 1 },
                    { new Guid("11111111-1111-1111-1111-000000000018"), "Sector 1", 18, 1, "Available", 1 },
                    { new Guid("11111111-1111-1111-1111-000000000019"), "Sector 1", 19, 1, "Available", 1 },
                    { new Guid("11111111-1111-1111-1111-000000000020"), "Sector 1", 20, 1, "Available", 1 },
                    { new Guid("11111111-1111-1111-1111-000000000021"), "Sector 1", 21, 1, "Available", 1 },
                    { new Guid("11111111-1111-1111-1111-000000000022"), "Sector 1", 22, 1, "Available", 1 },
                    { new Guid("11111111-1111-1111-1111-000000000023"), "Sector 1", 23, 1, "Available", 1 },
                    { new Guid("11111111-1111-1111-1111-000000000024"), "Sector 1", 24, 1, "Available", 1 },
                    { new Guid("11111111-1111-1111-1111-000000000025"), "Sector 1", 25, 1, "Available", 1 },
                    { new Guid("11111111-1111-1111-1111-000000000026"), "Sector 1", 26, 1, "Available", 1 },
                    { new Guid("11111111-1111-1111-1111-000000000027"), "Sector 1", 27, 1, "Available", 1 },
                    { new Guid("11111111-1111-1111-1111-000000000028"), "Sector 1", 28, 1, "Available", 1 },
                    { new Guid("11111111-1111-1111-1111-000000000029"), "Sector 1", 29, 1, "Available", 1 },
                    { new Guid("11111111-1111-1111-1111-000000000030"), "Sector 1", 30, 1, "Available", 1 },
                    { new Guid("11111111-1111-1111-1111-000000000031"), "Sector 1", 31, 1, "Available", 1 },
                    { new Guid("11111111-1111-1111-1111-000000000032"), "Sector 1", 32, 1, "Available", 1 },
                    { new Guid("11111111-1111-1111-1111-000000000033"), "Sector 1", 33, 1, "Available", 1 },
                    { new Guid("11111111-1111-1111-1111-000000000034"), "Sector 1", 34, 1, "Available", 1 },
                    { new Guid("11111111-1111-1111-1111-000000000035"), "Sector 1", 35, 1, "Available", 1 },
                    { new Guid("11111111-1111-1111-1111-000000000036"), "Sector 1", 36, 1, "Available", 1 },
                    { new Guid("11111111-1111-1111-1111-000000000037"), "Sector 1", 37, 1, "Available", 1 },
                    { new Guid("11111111-1111-1111-1111-000000000038"), "Sector 1", 38, 1, "Available", 1 },
                    { new Guid("11111111-1111-1111-1111-000000000039"), "Sector 1", 39, 1, "Available", 1 },
                    { new Guid("11111111-1111-1111-1111-000000000040"), "Sector 1", 40, 1, "Available", 1 },
                    { new Guid("11111111-1111-1111-1111-000000000041"), "Sector 1", 41, 1, "Available", 1 },
                    { new Guid("11111111-1111-1111-1111-000000000042"), "Sector 1", 42, 1, "Available", 1 },
                    { new Guid("11111111-1111-1111-1111-000000000043"), "Sector 1", 43, 1, "Available", 1 },
                    { new Guid("11111111-1111-1111-1111-000000000044"), "Sector 1", 44, 1, "Available", 1 },
                    { new Guid("11111111-1111-1111-1111-000000000045"), "Sector 1", 45, 1, "Available", 1 },
                    { new Guid("11111111-1111-1111-1111-000000000046"), "Sector 1", 46, 1, "Available", 1 },
                    { new Guid("11111111-1111-1111-1111-000000000047"), "Sector 1", 47, 1, "Available", 1 },
                    { new Guid("11111111-1111-1111-1111-000000000048"), "Sector 1", 48, 1, "Available", 1 },
                    { new Guid("11111111-1111-1111-1111-000000000049"), "Sector 1", 49, 1, "Available", 1 },
                    { new Guid("11111111-1111-1111-1111-000000000050"), "Sector 1", 50, 1, "Available", 1 },
                    { new Guid("22222222-2222-2222-2222-000000000001"), "Sector 2", 1, 2, "Available", 1 },
                    { new Guid("22222222-2222-2222-2222-000000000002"), "Sector 2", 2, 2, "Available", 1 },
                    { new Guid("22222222-2222-2222-2222-000000000003"), "Sector 2", 3, 2, "Available", 1 },
                    { new Guid("22222222-2222-2222-2222-000000000004"), "Sector 2", 4, 2, "Available", 1 },
                    { new Guid("22222222-2222-2222-2222-000000000005"), "Sector 2", 5, 2, "Available", 1 },
                    { new Guid("22222222-2222-2222-2222-000000000006"), "Sector 2", 6, 2, "Available", 1 },
                    { new Guid("22222222-2222-2222-2222-000000000007"), "Sector 2", 7, 2, "Available", 1 },
                    { new Guid("22222222-2222-2222-2222-000000000008"), "Sector 2", 8, 2, "Available", 1 },
                    { new Guid("22222222-2222-2222-2222-000000000009"), "Sector 2", 9, 2, "Available", 1 },
                    { new Guid("22222222-2222-2222-2222-000000000010"), "Sector 2", 10, 2, "Available", 1 },
                    { new Guid("22222222-2222-2222-2222-000000000011"), "Sector 2", 11, 2, "Available", 1 },
                    { new Guid("22222222-2222-2222-2222-000000000012"), "Sector 2", 12, 2, "Available", 1 },
                    { new Guid("22222222-2222-2222-2222-000000000013"), "Sector 2", 13, 2, "Available", 1 },
                    { new Guid("22222222-2222-2222-2222-000000000014"), "Sector 2", 14, 2, "Available", 1 },
                    { new Guid("22222222-2222-2222-2222-000000000015"), "Sector 2", 15, 2, "Available", 1 },
                    { new Guid("22222222-2222-2222-2222-000000000016"), "Sector 2", 16, 2, "Available", 1 },
                    { new Guid("22222222-2222-2222-2222-000000000017"), "Sector 2", 17, 2, "Available", 1 },
                    { new Guid("22222222-2222-2222-2222-000000000018"), "Sector 2", 18, 2, "Available", 1 },
                    { new Guid("22222222-2222-2222-2222-000000000019"), "Sector 2", 19, 2, "Available", 1 },
                    { new Guid("22222222-2222-2222-2222-000000000020"), "Sector 2", 20, 2, "Available", 1 },
                    { new Guid("22222222-2222-2222-2222-000000000021"), "Sector 2", 21, 2, "Available", 1 },
                    { new Guid("22222222-2222-2222-2222-000000000022"), "Sector 2", 22, 2, "Available", 1 },
                    { new Guid("22222222-2222-2222-2222-000000000023"), "Sector 2", 23, 2, "Available", 1 },
                    { new Guid("22222222-2222-2222-2222-000000000024"), "Sector 2", 24, 2, "Available", 1 },
                    { new Guid("22222222-2222-2222-2222-000000000025"), "Sector 2", 25, 2, "Available", 1 },
                    { new Guid("22222222-2222-2222-2222-000000000026"), "Sector 2", 26, 2, "Available", 1 },
                    { new Guid("22222222-2222-2222-2222-000000000027"), "Sector 2", 27, 2, "Available", 1 },
                    { new Guid("22222222-2222-2222-2222-000000000028"), "Sector 2", 28, 2, "Available", 1 },
                    { new Guid("22222222-2222-2222-2222-000000000029"), "Sector 2", 29, 2, "Available", 1 },
                    { new Guid("22222222-2222-2222-2222-000000000030"), "Sector 2", 30, 2, "Available", 1 },
                    { new Guid("22222222-2222-2222-2222-000000000031"), "Sector 2", 31, 2, "Available", 1 },
                    { new Guid("22222222-2222-2222-2222-000000000032"), "Sector 2", 32, 2, "Available", 1 },
                    { new Guid("22222222-2222-2222-2222-000000000033"), "Sector 2", 33, 2, "Available", 1 },
                    { new Guid("22222222-2222-2222-2222-000000000034"), "Sector 2", 34, 2, "Available", 1 },
                    { new Guid("22222222-2222-2222-2222-000000000035"), "Sector 2", 35, 2, "Available", 1 },
                    { new Guid("22222222-2222-2222-2222-000000000036"), "Sector 2", 36, 2, "Available", 1 },
                    { new Guid("22222222-2222-2222-2222-000000000037"), "Sector 2", 37, 2, "Available", 1 },
                    { new Guid("22222222-2222-2222-2222-000000000038"), "Sector 2", 38, 2, "Available", 1 },
                    { new Guid("22222222-2222-2222-2222-000000000039"), "Sector 2", 39, 2, "Available", 1 },
                    { new Guid("22222222-2222-2222-2222-000000000040"), "Sector 2", 40, 2, "Available", 1 },
                    { new Guid("22222222-2222-2222-2222-000000000041"), "Sector 2", 41, 2, "Available", 1 },
                    { new Guid("22222222-2222-2222-2222-000000000042"), "Sector 2", 42, 2, "Available", 1 },
                    { new Guid("22222222-2222-2222-2222-000000000043"), "Sector 2", 43, 2, "Available", 1 },
                    { new Guid("22222222-2222-2222-2222-000000000044"), "Sector 2", 44, 2, "Available", 1 },
                    { new Guid("22222222-2222-2222-2222-000000000045"), "Sector 2", 45, 2, "Available", 1 },
                    { new Guid("22222222-2222-2222-2222-000000000046"), "Sector 2", 46, 2, "Available", 1 },
                    { new Guid("22222222-2222-2222-2222-000000000047"), "Sector 2", 47, 2, "Available", 1 },
                    { new Guid("22222222-2222-2222-2222-000000000048"), "Sector 2", 48, 2, "Available", 1 },
                    { new Guid("22222222-2222-2222-2222-000000000049"), "Sector 2", 49, 2, "Available", 1 },
                    { new Guid("22222222-2222-2222-2222-000000000050"), "Sector 2", 50, 2, "Available", 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AUDIT_LOG_UserId",
                table: "AUDIT_LOG",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_RESERVATION_SeatId",
                table: "RESERVATION",
                column: "SeatId");

            migrationBuilder.CreateIndex(
                name: "IX_RESERVATION_UserId",
                table: "RESERVATION",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_SEAT_SectorId",
                table: "SEAT",
                column: "SectorId");

            migrationBuilder.CreateIndex(
                name: "IX_SECTOR_EventId",
                table: "SECTOR",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_USER_Email",
                table: "USER",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AUDIT_LOG");

            migrationBuilder.DropTable(
                name: "RESERVATION");

            migrationBuilder.DropTable(
                name: "SEAT");

            migrationBuilder.DropTable(
                name: "USER");

            migrationBuilder.DropTable(
                name: "SECTOR");

            migrationBuilder.DropTable(
                name: "EVENT");
        }
    }
}
