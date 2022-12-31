using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TakeASeat.Data.DatabaseContext.Seeds
{
    public class ShowSeed : IEntityTypeConfiguration<Show>
    {
        public void Configure(EntityTypeBuilder<Show> builder)
        {
            builder.HasData(
                // "Pink Panther The Movie"
                new Show
                {
                    Id = 1,
                    Date= new DateTime(2023, 1, 21, 21, 0, 0),
                    Description = "Night Showing",
                    EventId= 1,
                },
                new Show
                {
                Id = 2,
                    Date = new DateTime(2023, 1, 30, 9, 0, 0),
                    Description = "Morning Showing",
                    EventId = 1,
                },
                new Show
                {
                    Id = 3,
                    Date = new DateTime(2023, 1, 2, 9, 0, 0),
                    Description = "Morning Showing",
                    EventId = 1,
                },

                // "Tennis Local League"
                new Show
                {
                Id = 4,
                    Date = new DateTime(2023, 1, 21, 19, 0, 0),
                    Description = "Gonzo vs Bonzo",
                    EventId = 2,
                },
                new Show
                {
                Id = 5,
                    Date = new DateTime(2023, 1, 23, 19, 0, 0),
                    Description = "GGG vs Canelo",
                    EventId = 2,
                },
                new Show
                {
                    Id = 6,
                    Date = new DateTime(2023, 1, 28, 19, 0, 0),
                    Description = "GGG vs Canelo II",
                    EventId = 2,
                },

                // "Cossacks 3 Championships"
                new Show
                {
                    Id = 7,
                    Date = new DateTime(2023, 1, 27, 16, 30, 0),
                    Description = "Semifinal match I",
                    EventId = 3,
                },
                new Show
                {
                    Id = 8,
                    Date = new DateTime(2023, 1, 28, 16, 30, 0),
                    Description = "Semifinal match I",
                    EventId = 3,
                },
                new Show
                {
                    Id = 9,
                    Date = new DateTime(2023, 1, 5, 16, 30, 0),
                    Description = "Final match",
                    EventId = 3,
                },

                // "Fitness for everyone"
                new Show
                {
                    Id = 10,
                    Date = new DateTime(2023, 1, 27, 16, 30, 0),
                    Description = "Morning Routine",
                    EventId = 4,
                },
                new Show
                {
                    Id = 11,
                    Date = new DateTime(2023, 1, 28, 16, 30, 0),
                    Description = "Morning Routine",
                    EventId = 4,
                },
                new Show
                {
                    Id = 12,
                    Date = new DateTime(2023, 1, 29, 16, 30, 0),
                    Description = "Morning Routine",
                    EventId = 4,
                },

                // "FIFA playroom"
                new Show
                {
                    Id = 13,
                    Date = new DateTime(2023, 1, 27, 19, 30, 0),
                    Description = "Casual Games",
                    EventId = 5,
                },
                new Show
                {
                    Id = 14,
                    Date = new DateTime(2023, 1, 30, 16, 30, 0),
                    Description = "Local Final",
                    EventId = 5,
                }

                );
        }

    }
}
