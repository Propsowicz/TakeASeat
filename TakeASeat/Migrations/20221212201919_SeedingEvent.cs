using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TakeASeat.Migrations
{
    public partial class SeedingEvent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_eventTagEventM2Ms_Events_EventId",
                table: "eventTagEventM2Ms");

            migrationBuilder.DropForeignKey(
                name: "FK_eventTagEventM2Ms_EventTags_EventTagId",
                table: "eventTagEventM2Ms");

            migrationBuilder.DropPrimaryKey(
                name: "PK_eventTagEventM2Ms",
                table: "eventTagEventM2Ms");

            migrationBuilder.RenameTable(
                name: "eventTagEventM2Ms",
                newName: "EventTagEventM2M");

            migrationBuilder.RenameIndex(
                name: "IX_eventTagEventM2Ms_EventTagId",
                table: "EventTagEventM2M",
                newName: "IX_EventTagEventM2M_EventTagId");

            migrationBuilder.RenameIndex(
                name: "IX_eventTagEventM2Ms_EventId",
                table: "EventTagEventM2M",
                newName: "IX_EventTagEventM2M_EventId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EventTagEventM2M",
                table: "EventTagEventM2M",
                column: "Id");

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
                table: "Events",
                columns: new[] { "Id", "Description", "Duration", "ImageUri", "Name", "Type" },
                values: new object[,]
                {
                    { 1, "Pink Panther does his things.", 80, "none", "Pink Panther The Movie", "Movie" },
                    { 2, "Mr Moon vs Tactical Beacon", 120, "none", "Tennis Match", "Sport" },
                    { 3, "Best of 3.", 180, "none", "Cossacks 3 Champnionship - Final", "E-Sport" }
                });

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

            migrationBuilder.AddForeignKey(
                name: "FK_EventTagEventM2M_Events_EventId",
                table: "EventTagEventM2M",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EventTagEventM2M_EventTags_EventTagId",
                table: "EventTagEventM2M",
                column: "EventTagId",
                principalTable: "EventTags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventTagEventM2M_Events_EventId",
                table: "EventTagEventM2M");

            migrationBuilder.DropForeignKey(
                name: "FK_EventTagEventM2M_EventTags_EventTagId",
                table: "EventTagEventM2M");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EventTagEventM2M",
                table: "EventTagEventM2M");

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

            migrationBuilder.RenameTable(
                name: "EventTagEventM2M",
                newName: "eventTagEventM2Ms");

            migrationBuilder.RenameIndex(
                name: "IX_EventTagEventM2M_EventTagId",
                table: "eventTagEventM2Ms",
                newName: "IX_eventTagEventM2Ms_EventTagId");

            migrationBuilder.RenameIndex(
                name: "IX_EventTagEventM2M_EventId",
                table: "eventTagEventM2Ms",
                newName: "IX_eventTagEventM2Ms_EventId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_eventTagEventM2Ms",
                table: "eventTagEventM2Ms",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_eventTagEventM2Ms_Events_EventId",
                table: "eventTagEventM2Ms",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_eventTagEventM2Ms_EventTags_EventTagId",
                table: "eventTagEventM2Ms",
                column: "EventTagId",
                principalTable: "EventTags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
