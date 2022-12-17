using TakeASeat.Models;
using TakeASeat.RequestUtils;

namespace TakeASeat.UserServices
{
    public interface IAuthManager
    {
        Task<bool> ValidateUser(LoginUserDTO userDTO);
        Task<string> CreateJWToken(LoginUserDTO userDTO);
        Task<string> RefreshJWToken(string username);
        Task<JWTokenRequest> VerifyRefreshToken(JWTokenRequest request);

    }
}
