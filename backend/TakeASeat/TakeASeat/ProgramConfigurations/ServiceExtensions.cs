using Microsoft.AspNetCore.Identity;
using TakeASeat.Data.DatabaseContext;
using TakeASeat.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Serilog;
using TakeASeat.ProgramConfigurations.DTO;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.DependencyInjection;

namespace TakeASeat.ProgramConfigurations
{   
    public static class ServiceExtensions
    {
        public static void ConfigureJWT(this IServiceCollection services, IConfiguration Configuration)
        {
            var jwtSettings = Configuration.GetSection("Jwt");
            var key = Configuration.GetSection("ApiAuthKey:API_KEY").Value;
            var build = services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(o =>
                {
                    o.TokenValidationParameters = new TokenValidationParameters
                    {                       
                        ValidateIssuer = true,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtSettings.GetSection("Issuer").Value,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
                    };
                });
        }

        public static void ConfigureIdentity(this IServiceCollection services)
        {
            var builder = services.AddIdentityCore<User>(option =>
            {
                option.User.RequireUniqueEmail = true;
                option.Password.RequiredLength = 8;
                option.Password.RequireDigit = true;

            });

            builder = new IdentityBuilder(builder.UserType, typeof(IdentityRole), services);
            builder.AddTokenProvider("TakeASeat", typeof(DataProtectorTokenProvider<User>));
            builder.AddRoles<IdentityRole>();
            builder.AddEntityFrameworkStores<DatabaseContext>();
            builder.AddDefaultTokenProviders();
            
        }

        public static void ConfigureCORS(this IServiceCollection services, string corsPolicy)
        {
            var builder = services.AddCors(option =>
            {
                option.AddPolicy(name: corsPolicy,
                                        policy =>
                                        {
                                            policy.WithOrigins("*").AllowAnyHeader().AllowAnyMethod();
                                        }
                );
            });
        }

        public static void ConfigureSerilog(this IHostBuilder host)
        {
            var builder = host.UseSerilog((ctx, lc) => lc
                                                    .WriteTo.Console()
                                                    .WriteTo.File(path: "c:\\c#logs\\TakeASeat\\logs\\log-.txt",
                                                    outputTemplate: "{Timestamp:yyyy-mm-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}",
                                                    rollingInterval: RollingInterval.Day,
                                                    restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Information
                                            )
            );
        }

        public static void ConfigureApiVersioning(this IServiceCollection services)
        {
            var builder = services.AddApiVersioning(option =>
            {
                option.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
                option.ReportApiVersions = true;
                option.AssumeDefaultVersionWhenUnspecified = true;
            });
        }

        public static void ConfigureSwaggerGen(this IServiceCollection services)
        {
            var builder = services.AddSwaggerGen(option =>
            option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "JWT Authorization header using /Bearer scheme/ " + 
                                "Example: 'Bearer zxc.asd.qwe'",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            }
            ));
        }     
    }
    public class PaymentServerData
    {       
        public string? PIN { get; set; }
        public string? ID { get; set; }
    }
    public class EmailProviderData
    {
        public string? PASSWORD { get; set; }
        public string? ADDRESS { get; set; }
    }
    public class ApiAuthKey
    {
        public string? API_KEY { get; set; } 
    }
}
