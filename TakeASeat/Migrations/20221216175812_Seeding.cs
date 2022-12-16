using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TakeASeat.Migrations
{
    public partial class Seeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Seats_Events_EventId",
                table: "Seats");

            migrationBuilder.RenameColumn(
                name: "EventId",
                table: "Seats",
                newName: "ShowId");

            migrationBuilder.RenameIndex(
                name: "IX_Seats_EventId",
                table: "Seats",
                newName: "IX_Seats_ShowId");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Shows",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Cossacks 3 Championships");

            migrationBuilder.AddForeignKey(
                name: "FK_Seats_Shows_ShowId",
                table: "Seats",
                column: "ShowId",
                principalTable: "Shows",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Seats_Shows_ShowId",
                table: "Seats");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Shows");

            migrationBuilder.RenameColumn(
                name: "ShowId",
                table: "Seats",
                newName: "EventId");

            migrationBuilder.RenameIndex(
                name: "IX_Seats_ShowId",
                table: "Seats",
                newName: "IX_Seats_EventId");

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Cossacks 3 Championships - Final");

            migrationBuilder.AddForeignKey(
                name: "FK_Seats_Events_EventId",
                table: "Seats",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
