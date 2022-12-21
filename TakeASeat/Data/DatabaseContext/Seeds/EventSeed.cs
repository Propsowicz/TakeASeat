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
                    Duration = 90,
                    Description = "Pink Panther does his things.",
                    ImageUri = "none",
                    EventTypeId = 1,
                    Place = "Moskwa Cinema",
                    CreatorId = "8e445865-a24d-4543-a6c6-9443d048cdb9"
                },
                new Event
                {
                    Id = 2,
                    Name = "Tennis Local League",
                    Duration = 120,
                    Description = "Tennis Amatour League",
                    ImageUri = "none",
                    EventTypeId = 2,
                    Place = "Tennis Wschodnia",
                    CreatorId = "8e445865-a24d-4543-a6c6-9443d048cdb9"
                },
                new Event
                {
                    Id = 3,
                    Name = "Cossacks 3 Championships",
                    Duration = 180,
                    Description = "Weekly e-sport tournament.",
                    ImageUri = "none",
                    EventTypeId = 3,
                    Place = "Moskwa Cinema",
                    CreatorId = "8e445865-a24d-4543-a6c6-9443d048cdb0"
                },
                new Event
                {
                    Id = 4,
                    Name = "Fitness for everyone",
                    Duration = 60,
                    Description = "Daily fitness showcase.",
                    ImageUri = "none",
                    EventTypeId = 2,
                    Place = "Town Hall",
                    CreatorId = "8e445865-a24d-4543-a6c6-9443d048cdb9"
                },
                new Event
                {
                    Id = 5,
                    Name = "FIFA playroom",
                    Duration = 90,
                    Description = "Winter FIFA tournament",
                    ImageUri = "none",
                    EventTypeId = 3,
                    Place = "Quest pub",
                    CreatorId = "8e445865-a24d-4543-a6c6-9443d048cdb0"
                }

                );
        }

    }
}
