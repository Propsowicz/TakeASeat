using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TakeASeat.Data.DatabaseContext.Seeds
{
    public class EventTagEventM2MSeed : IEntityTypeConfiguration<EventTagEventM2M>
    {
        public void Configure(EntityTypeBuilder<EventTagEventM2M> builder)
        {
            builder.HasData(
                new EventTagEventM2M
                {
                    Id= 1,
                    EventId= 1,
                    EventTagId= 1,
                },
                new EventTagEventM2M
                {
                    Id = 2,
                    EventId = 1,
                    EventTagId = 2,
                },
                new EventTagEventM2M
                {
                    Id = 3,
                    EventId = 1,
                    EventTagId = 5,
                },
                new EventTagEventM2M
                {
                    Id = 4,
                    EventId = 2,
                    EventTagId = 3,
                },
                new EventTagEventM2M
                {
                    Id = 5,
                    EventId = 2,
                    EventTagId = 4,
                },
                new EventTagEventM2M
                {
                    Id = 6,
                    EventId = 3,
                    EventTagId = 3,
                },
                new EventTagEventM2M
                {
                    Id = 7,
                    EventId = 4,
                    EventTagId = 2,
                },
                new EventTagEventM2M
                {
                    Id = 8,
                    EventId = 4,
                    EventTagId = 4,
                },
                new EventTagEventM2M
                {
                    Id = 9,
                    EventId = 5,
                    EventTagId = 3,
                },
                new EventTagEventM2M
                {
                    Id = 10,
                    EventId = 5,
                    EventTagId = 4,
                }
                );
        }

    }
}
