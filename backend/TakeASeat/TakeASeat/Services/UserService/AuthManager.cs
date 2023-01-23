using AutoMapper;
using Microsoft.AspNetCore.Identity;
using TakeASeat.Data;
using TakeASeat.Models;
using TakeASeat.RequestUtils;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using TakeASeat.ProgramConfigurations.DTO;
using Microsoft.Extensions.Options;
using TakeASeat.ProgramConfigurations;

namespace TakeASeat.Services.UserService

{
    public class AuthManager : IAuthManager

    {
        private readonly string loginProvider = "TakeASeat";
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly ApiAuthKey _key;
        private User _user;

        public AuthManager(UserManager<User> userManager, IConfiguration configuration, IMapper mapper, IOptions<ApiAuthKey> options)
        {
            _userManager = userManager;
            _configuration = configuration;
            _mapper = mapper;
            _key = options.Value;
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
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key.API_KEY));

            return new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
        }

        private async Task<List<Claim>> GetClaims(LoginUserDTO userDTO)
        {
            var _user = await _userManager.FindByNameAsync(userDTO.UserName);
            var claims = new List<Claim>
            {
                new Claim("UserName", _user.UserName),
                new Claim("FirstName", _user.FirstName),
                new Claim("LastName", _user.LastName),
                new Claim("Email", _user.Email),
                new Claim("UserId", _user.Id)
            };

            var userRoles = await _userManager.GetRolesAsync(_user);
            foreach (var role in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
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
            _user = await _userManager.FindByNameAsync(userDTO.UserName);
            await _userManager.RemoveAuthenticationTokenAsync(_user, loginProvider, "RefreshToken");            
            var newRefreshToken = await _userManager.GenerateUserTokenAsync(_user, loginProvider, "RefreshToken");            
            await _userManager.SetAuthenticationTokenAsync(_user, loginProvider, "RefreshToken", newRefreshToken);
           
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
            var userDTO = _mapper.Map<LoginUserDTO>(_user);

            var isTokenValid = await _userManager.VerifyUserTokenAsync(_user, loginProvider, "RefreshToken", request.RefreshJWToken);
            if (isTokenValid)
            {
                return new JWTokenRequest { AccessJWToken = await CreateAccessJWToken(userDTO), RefreshJWToken = await CreateRefreshJWToken(userDTO) };
            }
            await _userManager.UpdateSecurityStampAsync(_user);

            return null;
        }
        
    }
}
