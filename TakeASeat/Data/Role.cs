using Microsoft.AspNetCore.Identity;

namespace TakeASeat.Data
{
    public class Role : IdentityRole
    {
        public virtual IList<UserRole> UserRoles { get; set; }
    }
}
