using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace TakeASeat.Data
{
    public class UserRole : IdentityUserRole<string>
    {
        public virtual User User { get; set; }

        public virtual Role Role { get; set; }
    }
}
