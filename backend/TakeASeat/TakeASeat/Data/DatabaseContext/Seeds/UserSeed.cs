using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TakeASeat.Data.DatabaseContext.Seeds
{
    public class UserSeed : IEntityTypeConfiguration<User>
    {        
        public void Configure(EntityTypeBuilder<User> builder)
        {
            var user_1 = new User 
            {
                FirstName = "George",
                LastName = "Flinston",
                Id = "8e445865-a24d-4543-a6c6-9443d048cdb9",
                UserName = "EventsInc",
                NormalizedUserName = "EVENTSINC",
                Email = "some@zx.com",
                NormalizedEmail = "SOME@ZX.COM",
                PhoneNumber = "1234567890",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                SecurityStamp = new Guid().ToString("D"),
            };
            var user_2 = new User
            {
                FirstName = "Logan",
                LastName = "Cappa",
                Id = "8e445865-a24d-4543-a6c6-9443d048cdb0",
                UserName = "User1",
                NormalizedUserName = "USER1",
                Email = "somenew@zx.com",
                NormalizedEmail = "SOMENEW@ZX.COM",
                PhoneNumber = "1234567890",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                SecurityStamp = new Guid().ToString("D"),
            };
            user_1.PasswordHash = PassGenerate(user_1);
            user_2.PasswordHash = PassGenerate(user_2);

            builder.HasData(user_1, user_2);               
        }
        public string PassGenerate(User user)
        {
            var passHash = new PasswordHasher<User>();
            return passHash.HashPassword(user, "Password123!");
        }
    }
}
