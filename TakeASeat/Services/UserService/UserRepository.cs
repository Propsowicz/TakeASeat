using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using TakeASeat.Data;
using TakeASeat.Data.DatabaseContext;
using TakeASeat.Models;

namespace TakeASeat.Services.UserService
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly DatabaseContext _context;

        public UserRepository(UserManager<User> userManager, DatabaseContext context, RoleManager<IdentityRole> roleManager)
        {
            _userManager= userManager;
            _roleManager= roleManager;
            _context= context;
        }

        public async Task<IList<IdentityRole>> GetRoles()
        {
            return await _roleManager.Roles.ToListAsync();
        }

        public async Task<IList<GetUsersToAdministratorPanel>> GetUsers()
        {       
            
                            

            return await _userManager.Users
                            .AsNoTracking()                            
                            .Select(u =>
                            new GetUsersToAdministratorPanel
                            {
                                UserName = u.UserName,
                                Id = u.Id,
                                FirstName = u.FirstName,
                                LastName = u.LastName,
                                Email = u.Email,
                                
                            })
                            .ToListAsync();
        }
    }
}
