using Microsoft.AspNetCore.Identity;
using TakeASeat.Data;
using TakeASeat.Models;
using TakeASeat.RequestUtils;
using X.PagedList;

namespace TakeASeat.Services.UserService
{
    public interface IUserRepository
    {
        Task<IPagedList<GetUsersToAdministratorPanelDTO>> GetUsers(RequestUserParams requestParams);
        Task<IList<IdentityRole>> GetRoles();
        Task ChangeRoles(EditUserRolesDTO userDTO);
        Task RemoveRoles(EditUserRolesDTO userDTO);
        Task AddToRoleNamedUser(User user);
        Task<int> GetUsersRecordsNumber();

    }
}
