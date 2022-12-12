using Microsoft.AspNetCore.Identity;

namespace TakeASeat.Data
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        

    }
}
