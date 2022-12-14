using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TakeASeat.Data.DatabaseContext.Seeds
{
    // it seems that i cannot seed m2m relations
    // https://learn.microsoft.com/en-us/ef/core/modeling/data-seeding#limitations-of-model-seed-data
    public class EventSeed : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.HasData(
                new Event
                {
                    Id = 1,
                    Name = "Pink Panther The Movie",
                    Type = "Movie",
                    Duration = 80,
                    Description = "Pink Panther does his things.",
                    ImageUri = "none",
                    EventTypeId = 1
                },
                new Event
                {
                    Id = 2,
                    Name = "Tennis Match",
                    Type = "Sport",
                    Duration = 120,
                    Description = "Mr Moon vs Tactical Beacon",
                    ImageUri = "none",
                    EventTypeId = 2
                },
                new Event
                {
                    Id = 3,
                    Name = "Cossacks 3 Championships - Final",
                    Type = "E-Sport",
                    Duration = 180,
                    Description = "Best of 3.",
                    ImageUri = "none",
                    EventTypeId = 3
                }

                );
        }

    }
}
