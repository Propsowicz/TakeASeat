using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TakeASeat.Data.DatabaseContext.Seeds
{
    public class UserRoleSeed : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasData(
                new IdentityRole
                {
                    Id = "1e445865-a24d-4543-a6c6-9443d048cdb9",
                    Name = "Administrator",
                    NormalizedName = "ADMINISTRATOR"
                },
                new IdentityRole
                {
                    Id = "2e445865-a24d-4543-a6c6-9443d048cdb9",
                    Name = "User",
                    NormalizedName = "USER"
                }
                ,
                new IdentityRole
                {
                    Id = "3e445865-a24d-4543-a6c6-9443d048cdb9",
                    Name = "Organizer",
                    NormalizedName = "ORGANIZER"
                }
                );
        }
    }
}
