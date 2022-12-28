using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TakeASeat.Migrations
{
    public partial class AddSeatList : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb0",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a5b3ee6c-e295-45b3-be0c-7cda2780a17c", "AQAAAAEAACcQAAAAEKEpfNyAy8Sic+ITDeDnG7UcDyxGk6mkSXPs7yzjRNQtoPm7AMxJ/3CJFJHtMW+a4A==", "b3914bef-2972-4c83-96d3-fb6ef8672bbf" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e7c4d0f8-33f7-449c-8832-fabbd97c4584", "AQAAAAEAACcQAAAAEANExFwPc+Q93I6XoLVajnyHF7bhaJCY6gXyAWp2qquWk1jbQEDI58er/pVlQVkGHg==", "a45b8eab-2fa8-4ffc-90b6-bfe319efd632" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb0",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5e6e09f7-eec3-4f7f-b3c4-a51c5ee16d1b", "AQAAAAEAACcQAAAAENAlInmt4VwnC7K9FLHYdSyfwxyJd2eizWSE6oXCoK99DcV0GegC9O+so59W1gvd0A==", "de54f36d-6ea3-4a31-bc9d-b88d853afac0" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "04343b26-1068-4b71-8ef6-ed97785dfd7f", "AQAAAAEAACcQAAAAEEHuY3/p/R5qYP+Qlzqn9ZvDOkPLTkEN3eazE2AU0rScPivO4g8vCgia+opeqi0Yxg==", "ecc7131d-c7f9-42fa-81c9-a327486dd50c" });
        }
    }
}
