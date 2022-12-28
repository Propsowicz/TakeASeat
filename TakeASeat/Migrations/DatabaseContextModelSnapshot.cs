﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TakeASeat.Data.DatabaseContext;

#nullable disable

namespace TakeASeat.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasDiscriminator<string>("Discriminator").HasValue("IdentityUser");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("TakeASeat.Data.Event", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("CreatorId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Duration")
                        .HasColumnType("int");

                    b.Property<string>("EventSlug")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("EventTypeId")
                        .HasColumnType("int");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Place")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CreatorId");

                    b.HasIndex("EventTypeId");

                    b.ToTable("Events");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatorId = "8e445865-a24d-4543-a6c6-9443d048cdb9",
                            Description = "Pink Panther does his things.",
                            Duration = 90,
                            EventSlug = "pink-panther-the-movie",
                            EventTypeId = 1,
                            ImageUrl = "https://cdn.pixabay.com/photo/2016/09/08/10/21/kermit-1653777_960_720.jpg",
                            Name = "Pink Panther The Movie",
                            Place = "Moskwa Cinema"
                        },
                        new
                        {
                            Id = 2,
                            CreatorId = "8e445865-a24d-4543-a6c6-9443d048cdb9",
                            Description = "Tennis Amatour League",
                            Duration = 120,
                            EventSlug = "tennis-local-league",
                            EventTypeId = 2,
                            ImageUrl = "https://cdn.pixabay.com/photo/2016/09/15/15/27/tennis-court-1671852__340.jpg",
                            Name = "Tennis Local League",
                            Place = "Tennis Wschodnia"
                        },
                        new
                        {
                            Id = 3,
                            CreatorId = "8e445865-a24d-4543-a6c6-9443d048cdb0",
                            Description = "Weekly e-sport tournament.",
                            Duration = 180,
                            EventSlug = "cossacks-3-championships",
                            EventTypeId = 3,
                            ImageUrl = "https://cdn.pixabay.com/photo/2022/06/12/21/31/helmet-7258913_960_720.png",
                            Name = "Cossacks 3 Championships",
                            Place = "Moskwa Cinema"
                        },
                        new
                        {
                            Id = 4,
                            CreatorId = "8e445865-a24d-4543-a6c6-9443d048cdb9",
                            Description = "Daily fitness showcase.",
                            Duration = 60,
                            EventSlug = "fitness-for-everyone",
                            EventTypeId = 2,
                            ImageUrl = "https://cdn.pixabay.com/photo/2017/07/02/19/24/dumbbells-2465478_960_720.jpg",
                            Name = "Fitness for everyone",
                            Place = "Town Hall"
                        },
                        new
                        {
                            Id = 5,
                            CreatorId = "8e445865-a24d-4543-a6c6-9443d048cdb0",
                            Description = "Winter FIFA tournament",
                            Duration = 90,
                            EventSlug = "fifa-playroom",
                            EventTypeId = 3,
                            ImageUrl = "https://cdn.pixabay.com/photo/2019/04/10/15/08/xbox-4117267_960_720.jpg",
                            Name = "FIFA playroom",
                            Place = "Quest pub"
                        });
                });

            modelBuilder.Entity("TakeASeat.Data.EventTag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("TagName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("EventTags");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            TagName = "#AnimatedMovie"
                        },
                        new
                        {
                            Id = 2,
                            TagName = "#FamilyFriendly"
                        },
                        new
                        {
                            Id = 3,
                            TagName = "#Competition"
                        },
                        new
                        {
                            Id = 4,
                            TagName = "#Sport"
                        },
                        new
                        {
                            Id = 5,
                            TagName = "#Comedy"
                        });
                });

            modelBuilder.Entity("TakeASeat.Data.EventTagEventM2M", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("EventId")
                        .HasColumnType("int");

                    b.Property<int>("EventTagId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EventId");

                    b.HasIndex("EventTagId");

                    b.ToTable("EventTagEventM2M");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            EventId = 1,
                            EventTagId = 1
                        },
                        new
                        {
                            Id = 2,
                            EventId = 1,
                            EventTagId = 2
                        },
                        new
                        {
                            Id = 3,
                            EventId = 1,
                            EventTagId = 5
                        },
                        new
                        {
                            Id = 4,
                            EventId = 2,
                            EventTagId = 3
                        },
                        new
                        {
                            Id = 5,
                            EventId = 2,
                            EventTagId = 4
                        },
                        new
                        {
                            Id = 6,
                            EventId = 3,
                            EventTagId = 3
                        },
                        new
                        {
                            Id = 7,
                            EventId = 4,
                            EventTagId = 2
                        },
                        new
                        {
                            Id = 8,
                            EventId = 4,
                            EventTagId = 4
                        },
                        new
                        {
                            Id = 9,
                            EventId = 5,
                            EventTagId = 3
                        },
                        new
                        {
                            Id = 10,
                            EventId = 5,
                            EventTagId = 4
                        });
                });

            modelBuilder.Entity("TakeASeat.Data.EventType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("EventTypes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Movie"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Sport"
                        },
                        new
                        {
                            Id = 3,
                            Name = "E-Sport"
                        });
                });

            modelBuilder.Entity("TakeASeat.Data.PaymentTransaction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<double>("TotalCost")
                        .HasColumnType("float");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("isAccepted")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("PaymentTransaction");
                });

            modelBuilder.Entity("TakeASeat.Data.Seat", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("Position")
                        .HasColumnType("int");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<int?>("ReservationId")
                        .HasColumnType("int");

                    b.Property<string>("Row")
                        .IsRequired()
                        .HasColumnType("nvarchar(1)");

                    b.Property<string>("SeatColor")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ShowId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ReservationId");

                    b.HasIndex("ShowId");

                    b.ToTable("Seats");
                });

            modelBuilder.Entity("TakeASeat.Data.SeatReservation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("ReservedTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("SoldTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("isReserved")
                        .HasColumnType("bit");

                    b.Property<bool>("isSold")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("SeatReservation");
                });

            modelBuilder.Entity("TakeASeat.Data.Show", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("EventId")
                        .HasColumnType("int");

                    b.Property<bool>("IsReadyToSell")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("EventId");

                    b.ToTable("Shows");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Date = new DateTime(2022, 12, 21, 21, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "Night Showing",
                            EventId = 1,
                            IsReadyToSell = false
                        },
                        new
                        {
                            Id = 2,
                            Date = new DateTime(2022, 12, 30, 9, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "Morning Showing",
                            EventId = 1,
                            IsReadyToSell = false
                        },
                        new
                        {
                            Id = 3,
                            Date = new DateTime(2023, 1, 2, 9, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "Morning Showing",
                            EventId = 1,
                            IsReadyToSell = false
                        },
                        new
                        {
                            Id = 4,
                            Date = new DateTime(2022, 12, 21, 19, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "Gonzo vs Bonzo",
                            EventId = 2,
                            IsReadyToSell = false
                        },
                        new
                        {
                            Id = 5,
                            Date = new DateTime(2022, 12, 23, 19, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "GGG vs Canelo",
                            EventId = 2,
                            IsReadyToSell = false
                        },
                        new
                        {
                            Id = 6,
                            Date = new DateTime(2022, 12, 28, 19, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "GGG vs Canelo II",
                            EventId = 2,
                            IsReadyToSell = false
                        },
                        new
                        {
                            Id = 7,
                            Date = new DateTime(2022, 12, 27, 16, 30, 0, 0, DateTimeKind.Unspecified),
                            Description = "Semifinal match I",
                            EventId = 3,
                            IsReadyToSell = false
                        },
                        new
                        {
                            Id = 8,
                            Date = new DateTime(2022, 12, 28, 16, 30, 0, 0, DateTimeKind.Unspecified),
                            Description = "Semifinal match I",
                            EventId = 3,
                            IsReadyToSell = false
                        },
                        new
                        {
                            Id = 9,
                            Date = new DateTime(2023, 1, 5, 16, 30, 0, 0, DateTimeKind.Unspecified),
                            Description = "Final match",
                            EventId = 3,
                            IsReadyToSell = false
                        },
                        new
                        {
                            Id = 10,
                            Date = new DateTime(2022, 12, 27, 16, 30, 0, 0, DateTimeKind.Unspecified),
                            Description = "Morning Routine",
                            EventId = 4,
                            IsReadyToSell = false
                        },
                        new
                        {
                            Id = 11,
                            Date = new DateTime(2022, 12, 28, 16, 30, 0, 0, DateTimeKind.Unspecified),
                            Description = "Morning Routine",
                            EventId = 4,
                            IsReadyToSell = false
                        },
                        new
                        {
                            Id = 12,
                            Date = new DateTime(2022, 12, 29, 16, 30, 0, 0, DateTimeKind.Unspecified),
                            Description = "Morning Routine",
                            EventId = 4,
                            IsReadyToSell = false
                        },
                        new
                        {
                            Id = 13,
                            Date = new DateTime(2022, 12, 27, 19, 30, 0, 0, DateTimeKind.Unspecified),
                            Description = "Casual Games",
                            EventId = 5,
                            IsReadyToSell = false
                        },
                        new
                        {
                            Id = 14,
                            Date = new DateTime(2022, 12, 30, 16, 30, 0, 0, DateTimeKind.Unspecified),
                            Description = "Local Final",
                            EventId = 5,
                            IsReadyToSell = false
                        });
                });

            modelBuilder.Entity("TakeASeat.Data.Ticket", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("SeatReservationId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SeatReservationId");

                    b.ToTable("Ticket");
                });

            modelBuilder.Entity("TakeASeat.Data.User", b =>
                {
                    b.HasBaseType("Microsoft.AspNetCore.Identity.IdentityUser");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasDiscriminator().HasValue("User");

                    b.HasData(
                        new
                        {
                            Id = "8e445865-a24d-4543-a6c6-9443d048cdb9",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "71aa6a5e-5f37-416a-9140-d7dd1e1a75a5",
                            EmailConfirmed = false,
                            LockoutEnabled = false,
                            PasswordHash = "AQAAAAEAACcQAAAAEOnarT8yDGdeIivM2WgbWcavOXQdBGreYTLa90ICMMOyrB1v148tOTl8aFhNxooO3A==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "8b1b3498-fcb9-4c5d-9831-f94cea889f52",
                            TwoFactorEnabled = false,
                            UserName = "Flinston",
                            FirstName = "George",
                            LastName = "Flinston"
                        },
                        new
                        {
                            Id = "8e445865-a24d-4543-a6c6-9443d048cdb0",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "94a6a332-6e86-401d-bc9d-c01f7d39ac77",
                            EmailConfirmed = false,
                            LockoutEnabled = false,
                            PasswordHash = "AQAAAAEAACcQAAAAECD31FSdTjWcWr4E1hprJw5TXnnERD6a/KDoxNsV2An13f3cFhS6s/9SwfoxgoSTCg==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "97cf8e98-594d-454e-81f8-df359803484c",
                            TwoFactorEnabled = false,
                            UserName = "LOG",
                            FirstName = "Logan",
                            LastName = "Capuchino"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TakeASeat.Data.Event", b =>
                {
                    b.HasOne("TakeASeat.Data.User", "Creator")
                        .WithMany()
                        .HasForeignKey("CreatorId");

                    b.HasOne("TakeASeat.Data.EventType", "EventType")
                        .WithMany()
                        .HasForeignKey("EventTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Creator");

                    b.Navigation("EventType");
                });

            modelBuilder.Entity("TakeASeat.Data.EventTagEventM2M", b =>
                {
                    b.HasOne("TakeASeat.Data.Event", "Event")
                        .WithMany("EventTags")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TakeASeat.Data.EventTag", "EventTag")
                        .WithMany()
                        .HasForeignKey("EventTagId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Event");

                    b.Navigation("EventTag");
                });

            modelBuilder.Entity("TakeASeat.Data.PaymentTransaction", b =>
                {
                    b.HasOne("TakeASeat.Data.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("TakeASeat.Data.Seat", b =>
                {
                    b.HasOne("TakeASeat.Data.SeatReservation", "SeatReservation")
                        .WithMany("Seats")
                        .HasForeignKey("ReservationId");

                    b.HasOne("TakeASeat.Data.Show", "Show")
                        .WithMany("Seats")
                        .HasForeignKey("ShowId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SeatReservation");

                    b.Navigation("Show");
                });

            modelBuilder.Entity("TakeASeat.Data.SeatReservation", b =>
                {
                    b.HasOne("TakeASeat.Data.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("TakeASeat.Data.Show", b =>
                {
                    b.HasOne("TakeASeat.Data.Event", "Event")
                        .WithMany("Shows")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Event");
                });

            modelBuilder.Entity("TakeASeat.Data.Ticket", b =>
                {
                    b.HasOne("TakeASeat.Data.SeatReservation", "SeatReservation")
                        .WithMany()
                        .HasForeignKey("SeatReservationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SeatReservation");
                });

            modelBuilder.Entity("TakeASeat.Data.Event", b =>
                {
                    b.Navigation("EventTags");

                    b.Navigation("Shows");
                });

            modelBuilder.Entity("TakeASeat.Data.SeatReservation", b =>
                {
                    b.Navigation("Seats");
                });

            modelBuilder.Entity("TakeASeat.Data.Show", b =>
                {
                    b.Navigation("Seats");
                });
#pragma warning restore 612, 618
        }
    }
}