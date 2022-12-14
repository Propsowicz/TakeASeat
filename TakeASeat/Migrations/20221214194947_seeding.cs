using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TakeASeat.Migrations
{
    public partial class seeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "EventTags",
                columns: new[] { "Id", "TagName" },
                values: new object[,]
                {
                    { 1, "Animated Movie" },
                    { 2, "Family Friendly" },
                    { 3, "Competition" },
                    { 4, "Sport" },
                    { 5, "Comedy" }
                });

            migrationBuilder.InsertData(
                table: "EventTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Movie" },
                    { 2, "Sport" },
                    { 3, "E-Sport" }
                });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "Id", "Description", "Duration", "EventTypeId", "ImageUri", "Name", "Type" },
                values: new object[] { 1, "Pink Panther does his things.", 80, 1, "none", "Pink Panther The Movie", "Movie" });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "Id", "Description", "Duration", "EventTypeId", "ImageUri", "Name", "Type" },
                values: new object[] { 2, "Mr Moon vs Tactical Beacon", 120, 2, "none", "Tennis Match", "Sport" });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "Id", "Description", "Duration", "EventTypeId", "ImageUri", "Name", "Type" },
                values: new object[] { 3, "Best of 3.", 180, 3, "none", "Cossacks 3 Championships - Final", "E-Sport" });

            migrationBuilder.InsertData(
                table: "EventTagEventM2M",
                columns: new[] { "Id", "EventId", "EventTagId" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 1, 2 },
                    { 3, 1, 5 },
                    { 4, 2, 3 },
                    { 5, 2, 4 },
                    { 6, 3, 3 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "EventTagEventM2M",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "EventTagEventM2M",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "EventTagEventM2M",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "EventTagEventM2M",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "EventTagEventM2M",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "EventTagEventM2M",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "EventTags",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "EventTags",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "EventTags",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "EventTags",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "EventTags",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "EventTypes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "EventTypes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "EventTypes",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
