using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TakeASeat.Migrations
{
    public partial class Seeding2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 2,
                column: "Description",
                value: "Amatour League");

            migrationBuilder.InsertData(
                table: "Shows",
                columns: new[] { "Id", "Date", "Description", "EventId" },
                values: new object[,]
                {
                    { 1, new DateTime(2022, 12, 16, 21, 0, 0, 0, DateTimeKind.Unspecified), "Night Showing", 1 },
                    { 2, new DateTime(2022, 12, 17, 9, 0, 0, 0, DateTimeKind.Unspecified), "Morning Showing", 1 },
                    { 3, new DateTime(2022, 12, 21, 19, 0, 0, 0, DateTimeKind.Unspecified), "Gonzo vs Bonzo", 2 },
                    { 4, new DateTime(2022, 12, 23, 19, 0, 0, 0, DateTimeKind.Unspecified), "GGG vs Canelo", 2 },
                    { 5, new DateTime(2022, 12, 28, 16, 30, 0, 0, DateTimeKind.Unspecified), "Final match", 3 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Shows",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Shows",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Shows",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Shows",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Shows",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 2,
                column: "Description",
                value: "Mr Moon vs Tactical Beacon");
        }
    }
}
