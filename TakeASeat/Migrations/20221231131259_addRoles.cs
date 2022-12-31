using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TakeASeat.Migrations
{
    public partial class addRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "32adf8c8-d930-424f-91fd-05a96a7407c1", "ff07fa01-923b-4ce4-9791-3a0f90f67b5e", "Organizer", "ORGANIZER" },
                    { "39f7f814-9ee0-42d5-b09a-6cddb638287c", "3c032fd7-13be-4dee-af73-956a1638f153", "User", "USER" },
                    { "47eeeba1-96bd-41c5-9620-a52278ec0acb", "623244aa-ce6a-452e-8119-be5a50e2fc27", "Administrator", "ADMINISTRATOR" }
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb0",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2d7c1ff3-aabf-40fe-bcd8-6fb7a4a539d9", "AQAAAAEAACcQAAAAEPPjQ5vo+ukajzn0YJwfep35SoYX7C+YBwtEjbRy7mOdiMSdbPiOo3yafp/svY88kw==", "eb130d1b-5668-4efb-954f-33ca069b20aa" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "dc6c6972-5a39-42a4-bf35-5a1e3f3c6c7f", "AQAAAAEAACcQAAAAEKrLgRnqv/QHtEspfxvUJu6X+48/1R+FKVBmxpQcPqc02ThTKF1VpYl7nW0/vA6pdQ==", "9c58438c-ad7c-4a4d-8bc3-ad1306d48c0a" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "32adf8c8-d930-424f-91fd-05a96a7407c1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "39f7f814-9ee0-42d5-b09a-6cddb638287c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "47eeeba1-96bd-41c5-9620-a52278ec0acb");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb0",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ce1b3c3a-26ba-43ec-b6b9-eb8ae9185e36", "AQAAAAEAACcQAAAAEKbhmpdGgXcEIvvbZ0pc8WInSyDEPYDaQPQewLZzdXl6O3nO5E8O1VWIAGca7/cmIg==", "fe1bff55-e124-4b3c-ad5a-6aebda1aa938" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "af447269-c477-42b2-8638-39f50097e32b", "AQAAAAEAACcQAAAAEOu/EOVcth3FSNMZMGK/wpQjCXbzMC73NHNvW2/KRwd3WujaNSkIhYcp8lJEpUD4zg==", "e00beed3-b9fc-4a70-a393-b536d91c86d6" });
        }
    }
}
