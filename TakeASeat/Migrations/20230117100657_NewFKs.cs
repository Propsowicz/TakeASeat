using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TakeASeat.Migrations
{
    public partial class NewFKs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SeatReservation_PaymentTransaction_PaymentTransactionId",
                table: "SeatReservation");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1a3b006e-24a8-42ff-bfc0-c30ed7c40044");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "778f28bd-3a35-4b6f-9695-9b8c403012b2");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ad6eed2f-ed9d-4dd0-9779-9f1069215d5a");

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

            migrationBuilder.AddForeignKey(
                name: "FK_SeatReservation_PaymentTransaction_PaymentTransactionId",
                table: "SeatReservation",
                column: "PaymentTransactionId",
                principalTable: "PaymentTransaction",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SeatReservation_PaymentTransaction_PaymentTransactionId",
                table: "SeatReservation");

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
                    { "1a3b006e-24a8-42ff-bfc0-c30ed7c40044", "782c7ba2-78b0-4a32-a4c1-93c1367cb68a", "Role", "User", "USER" },
                    { "778f28bd-3a35-4b6f-9695-9b8c403012b2", "c32f6fc2-5320-432a-b348-04d02b3e6693", "Role", "Administrator", "ADMINISTRATOR" },
                    { "ad6eed2f-ed9d-4dd0-9779-9f1069215d5a", "5e8910ca-fb29-4aa2-b799-d7b25bc24129", "Role", "Organizer", "ORGANIZER" }
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb0",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ce345e7f-d49e-4558-9076-50d626356f43", "AQAAAAEAACcQAAAAEFH+dJw8S8ebZGOm/WT0MdcTkKp4Mf3v2Ww/GVoiyrVFdq9ig47t4SN5hPzt2Cp1vA==", "53c1ddee-7d68-4b5e-b8ed-c2eb92ba0269" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e4b81c0d-84d6-4576-954a-0556e4f83ab0", "AQAAAAEAACcQAAAAEM9AEQQUTRyuZ2+P/D/9dDNEM7+vRgMefkp1i6zqTim5e/4ipwvk1o1Pp/yP6OC2OA==", "f0b09964-2c3d-4188-9210-ecc379102f13" });

            migrationBuilder.AddForeignKey(
                name: "FK_SeatReservation_PaymentTransaction_PaymentTransactionId",
                table: "SeatReservation",
                column: "PaymentTransactionId",
                principalTable: "PaymentTransaction",
                principalColumn: "Id");
        }
    }
}
