using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TakeASeat.Data.DatabaseContext.Seeds
{
    public class EventTypeSeed : IEntityTypeConfiguration<EventType>
    {
        public void Configure(EntityTypeBuilder<EventType> builder)
        {
            builder.HasData(
                new EventType
                {
                    Id = 1,
                    Name = "Movie"
                },
                new EventType
                {
                    Id = 2,
                    Name = "Sport"
                },
                new EventType
                {
                    Id = 3,
                    Name = "E-Sport"
                }
                );
        }
    }
}
