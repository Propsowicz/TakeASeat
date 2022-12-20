using TakeASeat.Models;
using TakeASeat.RequestUtils;

namespace TakeASeat.Services.UserService
{
    public interface IAuthManager
    {
        Task<bool> ValidateUser(LoginUserDTO userDTO);
        Task<string> CreateAccessJWToken(LoginUserDTO userDTO);
        Task<string> CreateRefreshJWToken(LoginUserDTO userDTO);
        Task<JWTokenRequest> VerifyRefreshToken(JWTokenRequest request);

    }
}
