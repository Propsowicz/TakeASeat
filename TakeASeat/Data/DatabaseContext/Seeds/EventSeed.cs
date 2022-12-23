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
                    ImageUrl = "https://cdn.pixabay.com/photo/2016/09/08/10/21/kermit-1653777_960_720.jpg",
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
                    ImageUrl = "https://cdn.pixabay.com/photo/2016/09/15/15/27/tennis-court-1671852__340.jpg",
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
                    ImageUrl = "https://cdn.pixabay.com/photo/2022/06/12/21/31/helmet-7258913_960_720.png",
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
                    ImageUrl = "https://cdn.pixabay.com/photo/2017/07/02/19/24/dumbbells-2465478_960_720.jpg",
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
                    ImageUrl = "https://cdn.pixabay.com/photo/2019/04/10/15/08/xbox-4117267_960_720.jpg",
                    EventTypeId = 3,
                    Place = "Quest pub",
                    CreatorId = "8e445865-a24d-4543-a6c6-9443d048cdb0"
                }

                );
        }

    }
}
