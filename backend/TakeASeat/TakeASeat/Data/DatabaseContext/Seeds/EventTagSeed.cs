using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TakeASeat.Data.DatabaseContext.Seeds
{
    public class EventTagSeed : IEntityTypeConfiguration<EventTag>
    {
        public void Configure(EntityTypeBuilder<EventTag> builder)
        {
            builder.HasData(
                new EventTag
                {
                    Id= 1,
                    TagName = "#AnimatedMovie"
                },
                new EventTag
                {
                    Id = 2,
                    TagName = "#FamilyFriendly"
                }, 
                new EventTag
                {
                    Id = 3,
                    TagName = "#Competition"
                },
                new EventTag
                {
                    Id = 4,
                    TagName = "#Sport"
                },
                new EventTag
                {
                    Id = 5,
                    TagName = "#Comedy"
                }
                );
        }

    }
}
