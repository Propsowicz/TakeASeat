using Microsoft.EntityFrameworkCore;
using TakeASeat.BackgroundServices;
using TakeASeat.Configurations;
using TakeASeat.Data.DatabaseContext;
using TakeASeat.IRepository;
using TakeASeat.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TakeASeat.UserServices;
using TakeASeat.Data;
using TakeASeat;

var builder = WebApplication.CreateBuilder(args);

// SERVICES

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Database link
builder.Services.AddDbContext<DatabaseContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Mapper conf
builder.Services.AddAutoMapper(typeof(MapperInitializer).Assembly);

// Dependency Injection
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IAuthManager, AuthManager>();

// Backgorund service
builder.Services.AddHostedService<ReleaseReservation>();

//JWT
builder.Services.AddAuthentication();
builder.Services.ConfigureIdentity();

//// set connection to appsettings
var jwtSettings = builder.Configuration.GetSection("Jwt");
//// get key
var key = AuthKey.AppKey;

builder.Services.AddAuthentication(option =>
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




// BUILDING

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
