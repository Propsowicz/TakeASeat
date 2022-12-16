using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TakeASeat.Data.DatabaseContext.Seeds
{
    public class ShowSeed : IEntityTypeConfiguration<Show>
    {
        public void Configure(EntityTypeBuilder<Show> builder)
        {
            builder.HasData(
                new Show
                {
                    Id = 1,
                    Date= new DateTime(2022, 12, 16, 21, 0, 0),
                    Description = "Night Showing",
                    EventId= 1,
                },
                new Show
                {
                Id = 2,
                    Date = new DateTime(2022, 12, 17, 9, 0, 0),
                    Description = "Morning Showing",
                    EventId = 1,
                },
                new Show
                {
                Id = 3,
                    Date = new DateTime(2022, 12, 21, 19, 0, 0),
                    Description = "Gonzo vs Bonzo",
                    EventId = 2,
                },
                new Show
                {
                Id = 4,
                    Date = new DateTime(2022, 12, 23, 19, 0, 0),
                    Description = "GGG vs Canelo",
                    EventId = 2,
                },
                new Show
                {
                Id = 5,
                    Date = new DateTime(2022, 12, 28, 16, 30, 0),
                    Description = "Final match",
                    EventId = 3,
                }
                );
        }

    }
}
