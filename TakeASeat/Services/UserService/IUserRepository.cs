using Microsoft.AspNetCore.Identity;
using TakeASeat.Data;
using TakeASeat.Models;

namespace TakeASeat.Services.UserService
{
    public interface IUserRepository
    {
        Task<IList<GetUsersToAdministratorPanel>> GetUsers();
        Task<IList<IdentityRole>> GetRoles();

    }
}
