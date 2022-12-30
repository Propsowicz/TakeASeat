using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TakeASeat.Migrations
{
    public partial class AppKeys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProtectedKeys",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Key = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProtectedKeys", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb0",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d8cef2a0-a8ea-41de-81ad-307a5ac2a135", "AQAAAAEAACcQAAAAEELFLL+dalhAcCZ8Q57x61o8R+HdFknDEK2dERKxzcnewhV52gfox2yGV4vVkEYljw==", "a4cbc470-74a2-4818-8240-1b095de0938f" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b9541160-8e5c-4fef-ab27-152145982825", "AQAAAAEAACcQAAAAEC4uIDIGuMKfr7bFewE91XvHS0ODLiUU5RuXGJgXHmXP7duK5o7A0XouYwcRVrV4VA==", "bc5079cf-e449-46cc-9331-0bb93f8d151a" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProtectedKeys");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb0",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "94a6a332-6e86-401d-bc9d-c01f7d39ac77", "AQAAAAEAACcQAAAAECD31FSdTjWcWr4E1hprJw5TXnnERD6a/KDoxNsV2An13f3cFhS6s/9SwfoxgoSTCg==", "97cf8e98-594d-454e-81f8-df359803484c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "71aa6a5e-5f37-416a-9140-d7dd1e1a75a5", "AQAAAAEAACcQAAAAEOnarT8yDGdeIivM2WgbWcavOXQdBGreYTLa90ICMMOyrB1v148tOTl8aFhNxooO3A==", "8b1b3498-fcb9-4c5d-9831-f94cea889f52" });
        }
    }
}
