using Microsoft.EntityFrameworkCore;
using TakeASeat.BackgroundServices;
using TakeASeat.Configurations;
using TakeASeat.Data.DatabaseContext;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TakeASeat.Data;
using System.Configuration;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Diagnostics;
using Serilog;
using TakeASeat.ProgramConfigurations;
using TakeASeat.Services.Generic;
using TakeASeat.Services.UserService;
using TakeASeat.Services.EventService;
using TakeASeat.Services.ShowService;

var builder = WebApplication.CreateBuilder(args);

// SERVICES
/// builder.Services.Configure*** are custom methods
builder.Services.AddControllers();

// CORS
var corsPolicy = "_corsPolicy";
builder.Services.ConfigureCORS(corsPolicy);

// LOGS
builder.Host.ConfigureSerilog();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Database link
builder.Services.AddDbContext<DatabaseContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Newton/JSON
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.WriteIndented = true;
});

// Mapper conf
builder.Services.AddAutoMapper(typeof(MapperInitializer).Assembly);

// Dependency Injection
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IAuthManager, AuthManager>();
builder.Services.AddScoped<IEventRepository, EventRepository>();
builder.Services.AddScoped<IShowRepository, ShowRepository>();

// Backgorund service
builder.Services.AddHostedService<ReleaseReservation>();

//JWT
builder.Services.AddAuthentication();
builder.Services.ConfigureIdentity();
builder.Services.ConfigureJWT(builder.Configuration);




// BUILDING

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.ConfigureGlobalExceptionHandler(app);

app.UseHttpsRedirection();

app.UseCors(corsPolicy);

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
