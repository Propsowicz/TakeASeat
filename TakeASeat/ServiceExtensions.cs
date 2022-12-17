using Microsoft.AspNetCore.Identity;
using TakeASeat.Data.DatabaseContext;
using TakeASeat.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace TakeASeat
{
    public static class ServiceExtensions
    {
        public static void ConfigureIdentity(this IServiceCollection services)
        {
            var builder = services.AddIdentityCore<User>(option =>
            {
                option.Password.RequireDigit = true;
            });

            builder = new IdentityBuilder(builder.UserType, typeof(IdentityRole), services);
            builder.AddTokenProvider("HotelList", typeof(DataProtectorTokenProvider<User>));
            builder.AddEntityFrameworkStores<DatabaseContext>().AddDefaultTokenProviders();
        }

        public static void ConfigureJWT(this IServiceCollection services, IConfiguration Configuration)
        {
            // set connection to appsettings
            var jwtSettings = Configuration.GetSection("Jwt");

            // get key from sytsem env (cmd as admin and: setx KEY "SECRET_KEY" /M)
            var key = Environment.GetEnvironmentVariable("KEY");

            // set jwt as auth method
            services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            })
                // validation options
                .AddJwtBearer(o =>
                {
                    o.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,  // validate if token is from our app
                        ValidateLifetime = true,    // exp date
                        ValidateIssuerSigningKey = true,    // validate SECRET_KEY
                        ValidIssuer = jwtSettings.GetSection("Issuer").Value,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))    // encoding and hashing the SECRET_KEY
                    };
                });
        }
    }
}
