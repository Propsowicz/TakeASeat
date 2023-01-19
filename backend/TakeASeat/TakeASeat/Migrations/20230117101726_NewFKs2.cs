using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TakeASeat.Migrations
{
    public partial class NewFKs2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "398a3e73-85f4-47f9-8b00-b0b839c1e986");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3c6b9542-f388-4d4f-9d07-aba5eb395fe5");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "df041ffd-666c-44c4-944e-122ab2e6e3fb");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Discriminator", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "15806b95-b7a5-4f14-bbbb-43d63b60a1a6", "570dae51-647d-474c-9eee-65f9155c45eb", "Role", "Organizer", "ORGANIZER" },
                    { "9ef7b03b-432e-4319-90de-13a6f3afa5f1", "a9329983-5bc0-464f-bb52-a3a4d270ed76", "Role", "Administrator", "ADMINISTRATOR" },
                    { "b94bfda4-97e7-429e-b7fe-3086becd8818", "21471440-5ba0-4a83-953d-df7bf71e31bf", "Role", "User", "USER" }
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb0",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6b62db22-aa81-499b-8baf-0c53614f55ce", "AQAAAAEAACcQAAAAEHRdnW2n6buGy9nsg4DWlyDNkG56vdJlIT/dvABOJWcT2qmWANZIbN5yIkuox1sdzQ==", "11107314-a417-4207-81a2-90cef9f3e734" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "618ec6bc-23aa-4385-9c4d-632969a2b3cb", "AQAAAAEAACcQAAAAEDGnKu7tZveK11rD7q0RxuueAoTiLFprSMTvL0g60mXqwwXqhLjcUr7WZ51wC5uiBg==", "22b3a8aa-4f3f-4f80-9421-933dfce4349b" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "15806b95-b7a5-4f14-bbbb-43d63b60a1a6");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9ef7b03b-432e-4319-90de-13a6f3afa5f1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b94bfda4-97e7-429e-b7fe-3086becd8818");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Discriminator", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "398a3e73-85f4-47f9-8b00-b0b839c1e986", "e54bbc37-c695-44a3-8450-662be68a5547", "Role", "Administrator", "ADMINISTRATOR" },
                    { "3c6b9542-f388-4d4f-9d07-aba5eb395fe5", "939138ed-aa08-4ab9-921e-1ca7ba417739", "Role", "User", "USER" },
                    { "df041ffd-666c-44c4-944e-122ab2e6e3fb", "6f416d04-55f8-437a-bd74-fba3035e1221", "Role", "Organizer", "ORGANIZER" }
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb0",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "122947d1-5679-462e-b0a9-764dc976e51d", "AQAAAAEAACcQAAAAEH5mV0LyLYR+Vt6XpCC8lW0CUT1NY0tX2/TNcSyK1nNwe7Xgao20FVh2CzHy/Zcczg==", "df5d9cf6-8224-43c1-9945-8b49b619f635" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8ccd26de-f929-4d6c-be36-4ef314df909c", "AQAAAAEAACcQAAAAEIbaujrzd7iBBsbbcC8pF1UbB0Jpa8yQgJ1cwAEcId6kJ/VVV0HxoBNQgc76vtB0/w==", "8c97fa52-f4f9-489a-b303-daa8ce574357" });
        }
    }
}
