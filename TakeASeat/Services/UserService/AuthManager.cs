using AutoMapper;
using Microsoft.AspNetCore.Identity;
using TakeASeat.Data;
using TakeASeat.Models;
using TakeASeat.RequestUtils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using TakeASeat.Configurations;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;


namespace TakeASeat.Services.UserService

{
    public class AuthManager : IAuthManager

    {
        private readonly string loginProvider = "TakeASeat";
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private User _user;
        private LoginUserDTO _userDTO;

        public AuthManager(UserManager<User> userManager, IConfiguration configuration, IMapper mapper)
        {
            _userManager = userManager;
            _configuration = configuration;
            _mapper = mapper;
        }

        public async Task<string> CreateAccessJWToken(LoginUserDTO userDTO)
        {
            var signingCredentials = GetSigningCredentials();
            var claims = await GetClaims(userDTO);
            var token = GenerateTokenOptions(signingCredentials, claims);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private SigningCredentials GetSigningCredentials()
        {
            var appKey = AuthKey.AppKey;
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(appKey));

            return new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
        }

        private async Task<List<Claim>> GetClaims(LoginUserDTO userDTO)
        {
            var _user = await _userManager.FindByNameAsync(userDTO.UserName);
            var claims = new List<Claim>
            {
                new Claim("UserName", _user.UserName),
                new Claim("UserId", _user.Id)
            };

            var userRoles = await _userManager.GetRolesAsync(_user);
            foreach (var role in userRoles)
            {
                claims.Add(new Claim("Role", role));
            }
            return claims;
        }

        private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
        {
            var jwtSettings = _configuration.GetSection("Jwt");

            var token = new JwtSecurityToken(
                issuer: jwtSettings.GetSection("Issuer").Value,
                claims: claims,
                expires: DateTime.Now.AddHours(3),
                signingCredentials: signingCredentials
                ); ;
            return token;
        }

        public async Task<string> CreateRefreshJWToken(LoginUserDTO userDTO)
        {
            _user = _mapper.Map<User>(userDTO);
            await _userManager.RemoveAuthenticationTokenAsync(_user, loginProvider, "RefreshToken");
            var newRefreshToken = await _userManager.GenerateUserTokenAsync(_user, loginProvider, "RefreshToken");
            //var result = await _userManager.SetAuthenticationTokenAsync(_user, loginProvider, "RefreshToken", newRefreshToken);
            return newRefreshToken;
        }

        public async Task<bool> ValidateUser(LoginUserDTO userDTO)
        {
            var _user = await _userManager.FindByNameAsync(userDTO.UserName);
            return _user != null && await _userManager.CheckPasswordAsync(_user, userDTO.Password);
        }

        public async Task<JWTokenRequest> VerifyRefreshToken(JWTokenRequest request)
        {
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var tokenContent = jwtSecurityTokenHandler.ReadJwtToken(request.AccessJWToken);
            var username = tokenContent.Claims.ToList().FirstOrDefault(t => t.Type == "UserName")?.Value;
            var _user = await _userManager.FindByNameAsync(username);
            _mapper.Map(_userDTO, _user);

            var isTokenValid = await _userManager.VerifyUserTokenAsync(_user, loginProvider, "RefreshToken", request.RefreshJWToken);
            if (isTokenValid)
            {
                return new JWTokenRequest { AccessJWToken = await CreateAccessJWToken(_userDTO), RefreshJWToken = await CreateRefreshJWToken(_userDTO) };
            }
            await _userManager.UpdateSecurityStampAsync(_user);

            return null;
        }
    }
}
