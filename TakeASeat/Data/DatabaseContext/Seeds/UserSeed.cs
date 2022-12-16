using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TakeASeat.Data.DatabaseContext.Seeds
{
    public class UserSeed : IEntityTypeConfiguration<User>
    {

        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasData(
                new User
                {           
                    Id = 1,
                    FirstName = "George",
                    LastName = "Flinston",
                                  
                },
                new User
                {
                    Id = 2,
                    FirstName = "Logan",
                    LastName = "Capuchino",
                    
                }
                );

        }
    }
}
