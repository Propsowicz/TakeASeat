using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TakeASeat.Data.DatabaseContext.Seeds
{
    public class UserSeed : IEntityTypeConfiguration<User>
    {
        
        public void Configure(EntityTypeBuilder<User> builder)
        {
            var hasher = new PasswordHasher<IdentityUser>();
            builder.HasData(
                new User
                {
                    Id = "8e445865-a24d-4543-a6c6-9443d048cdb9",
                    FirstName = "George",
                    LastName = "Flinston",
                    UserName = "Flinston",
                    PasswordHash = hasher.HashPassword(null, "Haslo123!")
                                  
                },
                new User
                {
                    Id = "8e445865-a24d-4543-a6c6-9443d048cdb0",
                    FirstName = "Logan",
                    LastName = "Capuchino",
                    UserName = "LOG",
                    PasswordHash = hasher.HashPassword(null, "Haslo123!")
                }
                );

        }
    }
}
