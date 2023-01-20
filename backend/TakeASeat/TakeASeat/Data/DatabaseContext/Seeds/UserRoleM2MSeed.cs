using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TakeASeat.Data.DatabaseContext.Seeds
{
    public class UserRoleM2MSeed : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.HasData(                
                new UserRole
                {
                    RoleId = "1e445865-a24d-4543-a6c6-9443d048cdb9",
                    UserId = "8e445865-a24d-4543-a6c6-9443d048cdb9"
                },
                new UserRole 
                {
                    RoleId = "2e445865-a24d-4543-a6c6-9443d048cdb9",
                    UserId = "8e445865-a24d-4543-a6c6-9443d048cdb0"
                }
                );
        }
    }
}
