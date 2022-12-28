using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TakeASeat.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EventTags",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TagName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventTags", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EventTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SeatReservation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    isReserved = table.Column<bool>(type: "bit", nullable: false),
                    ReservedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    isSold = table.Column<bool>(type: "bit", nullable: false),
                    SoldTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeatReservation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SeatReservation_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Duration = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Place = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EventSlug = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EventTypeId = table.Column<int>(type: "int", nullable: false),
                    CreatorId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Events_AspNetUsers_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Events_EventTypes_EventTypeId",
                        column: x => x.EventTypeId,
                        principalTable: "EventTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ticket",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SeatReservationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ticket", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ticket_SeatReservation_SeatReservationId",
                        column: x => x.SeatReservationId,
                        principalTable: "SeatReservation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EventTagEventM2M",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventId = table.Column<int>(type: "int", nullable: false),
                    EventTagId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventTagEventM2M", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EventTagEventM2M_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventTagEventM2M_EventTags_EventTagId",
                        column: x => x.EventTagId,
                        principalTable: "EventTags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Shows",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsReadyToSell = table.Column<bool>(type: "bit", nullable: false),
                    EventId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shows", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Shows_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Seats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Row = table.Column<string>(type: "nvarchar(1)", nullable: false),
                    Position = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    SeatColor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReservationId = table.Column<int>(type: "int", nullable: true),
                    ShowId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Seats_SeatReservation_ReservationId",
                        column: x => x.ReservationId,
                        principalTable: "SeatReservation",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Seats_Shows_ShowId",
                        column: x => x.ShowId,
                        principalTable: "Shows",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "8e445865-a24d-4543-a6c6-9443d048cdb0", 0, "5e6e09f7-eec3-4f7f-b3c4-a51c5ee16d1b", "User", null, false, "Logan", "Capuchino", false, null, null, null, "AQAAAAEAACcQAAAAENAlInmt4VwnC7K9FLHYdSyfwxyJd2eizWSE6oXCoK99DcV0GegC9O+so59W1gvd0A==", null, false, "de54f36d-6ea3-4a31-bc9d-b88d853afac0", false, "LOG" },
                    { "8e445865-a24d-4543-a6c6-9443d048cdb9", 0, "04343b26-1068-4b71-8ef6-ed97785dfd7f", "User", null, false, "George", "Flinston", false, null, null, null, "AQAAAAEAACcQAAAAEEHuY3/p/R5qYP+Qlzqn9ZvDOkPLTkEN3eazE2AU0rScPivO4g8vCgia+opeqi0Yxg==", null, false, "ecc7131d-c7f9-42fa-81c9-a327486dd50c", false, "Flinston" }
                });

            migrationBuilder.InsertData(
                table: "EventTags",
                columns: new[] { "Id", "TagName" },
                values: new object[,]
                {
                    { 1, "#AnimatedMovie" },
                    { 2, "#FamilyFriendly" },
                    { 3, "#Competition" },
                    { 4, "#Sport" },
                    { 5, "#Comedy" }
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
                columns: new[] { "Id", "CreatorId", "Description", "Duration", "EventSlug", "EventTypeId", "ImageUrl", "Name", "Place" },
                values: new object[,]
                {
                    { 1, "8e445865-a24d-4543-a6c6-9443d048cdb9", "Pink Panther does his things.", 90, "pink-panther-the-movie", 1, "https://cdn.pixabay.com/photo/2016/09/08/10/21/kermit-1653777_960_720.jpg", "Pink Panther The Movie", "Moskwa Cinema" },
                    { 2, "8e445865-a24d-4543-a6c6-9443d048cdb9", "Tennis Amatour League", 120, "tennis-local-league", 2, "https://cdn.pixabay.com/photo/2016/09/15/15/27/tennis-court-1671852__340.jpg", "Tennis Local League", "Tennis Wschodnia" },
                    { 3, "8e445865-a24d-4543-a6c6-9443d048cdb0", "Weekly e-sport tournament.", 180, "cossacks-3-championships", 3, "https://cdn.pixabay.com/photo/2022/06/12/21/31/helmet-7258913_960_720.png", "Cossacks 3 Championships", "Moskwa Cinema" },
                    { 4, "8e445865-a24d-4543-a6c6-9443d048cdb9", "Daily fitness showcase.", 60, "fitness-for-everyone", 2, "https://cdn.pixabay.com/photo/2017/07/02/19/24/dumbbells-2465478_960_720.jpg", "Fitness for everyone", "Town Hall" },
                    { 5, "8e445865-a24d-4543-a6c6-9443d048cdb0", "Winter FIFA tournament", 90, "fifa-playroom", 3, "https://cdn.pixabay.com/photo/2019/04/10/15/08/xbox-4117267_960_720.jpg", "FIFA playroom", "Quest pub" }
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
                    { 6, 3, 3 },
                    { 7, 4, 2 },
                    { 8, 4, 4 },
                    { 9, 5, 3 },
                    { 10, 5, 4 }
                });

            migrationBuilder.InsertData(
                table: "Shows",
                columns: new[] { "Id", "Date", "Description", "EventId", "IsReadyToSell" },
                values: new object[,]
                {
                    { 1, new DateTime(2022, 12, 21, 21, 0, 0, 0, DateTimeKind.Unspecified), "Night Showing", 1, false },
                    { 2, new DateTime(2022, 12, 30, 9, 0, 0, 0, DateTimeKind.Unspecified), "Morning Showing", 1, false },
                    { 3, new DateTime(2023, 1, 2, 9, 0, 0, 0, DateTimeKind.Unspecified), "Morning Showing", 1, false },
                    { 4, new DateTime(2022, 12, 21, 19, 0, 0, 0, DateTimeKind.Unspecified), "Gonzo vs Bonzo", 2, false },
                    { 5, new DateTime(2022, 12, 23, 19, 0, 0, 0, DateTimeKind.Unspecified), "GGG vs Canelo", 2, false },
                    { 6, new DateTime(2022, 12, 28, 19, 0, 0, 0, DateTimeKind.Unspecified), "GGG vs Canelo II", 2, false },
                    { 7, new DateTime(2022, 12, 27, 16, 30, 0, 0, DateTimeKind.Unspecified), "Semifinal match I", 3, false },
                    { 8, new DateTime(2022, 12, 28, 16, 30, 0, 0, DateTimeKind.Unspecified), "Semifinal match I", 3, false },
                    { 9, new DateTime(2023, 1, 5, 16, 30, 0, 0, DateTimeKind.Unspecified), "Final match", 3, false },
                    { 10, new DateTime(2022, 12, 27, 16, 30, 0, 0, DateTimeKind.Unspecified), "Morning Routine", 4, false },
                    { 11, new DateTime(2022, 12, 28, 16, 30, 0, 0, DateTimeKind.Unspecified), "Morning Routine", 4, false },
                    { 12, new DateTime(2022, 12, 29, 16, 30, 0, 0, DateTimeKind.Unspecified), "Morning Routine", 4, false },
                    { 13, new DateTime(2022, 12, 27, 19, 30, 0, 0, DateTimeKind.Unspecified), "Casual Games", 5, false },
                    { 14, new DateTime(2022, 12, 30, 16, 30, 0, 0, DateTimeKind.Unspecified), "Local Final", 5, false }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Events_CreatorId",
                table: "Events",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_EventTypeId",
                table: "Events",
                column: "EventTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_EventTagEventM2M_EventId",
                table: "EventTagEventM2M",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_EventTagEventM2M_EventTagId",
                table: "EventTagEventM2M",
                column: "EventTagId");

            migrationBuilder.CreateIndex(
                name: "IX_SeatReservation_UserId",
                table: "SeatReservation",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Seats_ReservationId",
                table: "Seats",
                column: "ReservationId");

            migrationBuilder.CreateIndex(
                name: "IX_Seats_ShowId",
                table: "Seats",
                column: "ShowId");

            migrationBuilder.CreateIndex(
                name: "IX_Shows_EventId",
                table: "Shows",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_SeatReservationId",
                table: "Ticket",
                column: "SeatReservationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "EventTagEventM2M");

            migrationBuilder.DropTable(
                name: "Seats");

            migrationBuilder.DropTable(
                name: "Ticket");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "EventTags");

            migrationBuilder.DropTable(
                name: "Shows");

            migrationBuilder.DropTable(
                name: "SeatReservation");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "EventTypes");
        }
    }
}
