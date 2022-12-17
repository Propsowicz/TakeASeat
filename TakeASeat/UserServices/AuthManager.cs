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


namespace TakeASeat.UserServices
{
    public class AuthManager : IAuthManager
         
    {
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private User _user;

        public AuthManager(UserManager<User> userManager, IConfiguration configuration, IMapper mapper)
        {
            _userManager = userManager;
            _configuration = configuration;
            _mapper = mapper;
        }



        public async Task<string> CreateJWToken(LoginUserDTO userDTO)
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
                new Claim("some", "asds")
            };

            var userRoles = await _userManager.GetRolesAsync(_user);
            foreach (var role in userRoles)
            {
                claims.Add(new Claim ( "Role", role ));
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

        public Task<string> RefreshJWToken(string username)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> ValidateUser(LoginUserDTO userDTO)
        {
            var _user = await _userManager.FindByNameAsync(userDTO.UserName);
            return (_user != null && await _userManager.CheckPasswordAsync(_user, userDTO.Password));
        }

        public Task<JWTokenRequest> VerifyRefreshToken(JWTokenRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
