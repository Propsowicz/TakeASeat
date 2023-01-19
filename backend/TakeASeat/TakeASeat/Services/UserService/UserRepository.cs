using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using TakeASeat.Data;
using TakeASeat.Data.DatabaseContext;
using TakeASeat.Models;
using TakeASeat.RequestUtils;
using X.PagedList;

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

        public async Task AddToRoleNamedUser(User user)
        {
            var roleId = await _context.Roles.FirstOrDefaultAsync(r => r.Name== "User");
            
            ArgumentNullException.ThrowIfNull(roleId);
            await _context.UserRoles.AddAsync(new UserRole { UserId = user.Id, RoleId = roleId.Id });
            await _context.SaveChangesAsync();
        }

        public async Task ChangeRoles(EditUserRolesDTO userDTO)
        {
            List<UserRole> tempUserRoles = new List<UserRole>() { };

            foreach(var userRole in userDTO.UserRoles) 
            {
                tempUserRoles.Add( new UserRole { RoleId = userRole.Id, UserId = userDTO.UserId });
            }

            ArgumentNullException.ThrowIfNull(tempUserRoles);

            await RemoveRoles(userDTO);
            await _context.UserRoles.AddRangeAsync(tempUserRoles);
            await _context.SaveChangesAsync();
        }
               
        public async Task<IList<IdentityRole>> GetRoles()
        {
            return await _roleManager.Roles.ToListAsync();
        }

        public async Task<IPagedList<GetUsersToAdministratorPanelDTO>> GetUsers(RequestUserParams requestParams)
        {
            return await _userManager.Users
                            .AsNoTracking()
                            .Where(u => u.UserName.Contains(requestParams.SearchString))
                            .Select(u =>
                            new GetUsersToAdministratorPanelDTO
                            {
                                UserName = u.UserName,
                                Id = u.Id,
                                FirstName = u.FirstName,
                                LastName = u.LastName,
                                Email = u.Email,
                                UserRoles = u.UserRoles.Select(r => 
                                new GetRoleDTO{
                                    Id = r.Role.Id,
                                    Name = r.Role.Name,                                    
                                })
                            })
                            .ToPagedListAsync(requestParams.PageNumber, requestParams.PageSize);
        }

        public async Task<int> GetUsersRecordsNumber()
        {
            return await _context.Users.CountAsync();
        }

        public async Task RemoveRoles(EditUserRolesDTO userDTO)
        {
            var query = _context.UserRoles.Where(r => r.UserId == userDTO.UserId);
            _context.UserRoles.RemoveRange(query);
        }
    }
}
