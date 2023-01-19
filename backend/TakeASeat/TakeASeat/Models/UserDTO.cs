namespace TakeASeat.Models
{
    public class LoginUserDTO
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class RegisterUserDTO
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }

    public class GetUserDTO 
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }

    public class GetUsersToAdministratorPanelDTO
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public IEnumerable<GetRoleDTO> UserRoles { get; set; }
    }

    public class EditUserRolesDTO
    {
        public string UserId { get; set; }
        public IEnumerable<GetRoleDTO> UserRoles { get; set; }

    }
}

