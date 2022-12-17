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
                    
                    FirstName = "George",
                    LastName = "Flinston",
                                  
                },
                new User
                {
                    
                    FirstName = "Logan",
                    LastName = "Capuchino",
                    
                }
                );

        }
    }
}
