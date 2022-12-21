namespace TakeASeat.Models
{
    public class LoginUserDTO
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class RegisterUserDTO : LoginUserDTO
    {
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
}

