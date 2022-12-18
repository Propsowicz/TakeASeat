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
using System.Configuration;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Diagnostics;
using Serilog;
using TakeASeat.ProgramConfigurations;

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

//app.UseExceptionHandler(error =>
//{
//    error.Run(async context =>
//    {
//        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
//        context.Response.ContentType = "application/json";
//        var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
//        if (contextFeature != null)
//        {
//            Log.Error($"Something went wrong in the {contextFeature.Error}");
//            await context.Response.WriteAsync(new ErrorProps
//            {
//                StatusCode = StatusCodes.Status500InternalServerError,
//                Message = "Internal server error. Please try again later."
//            }.ToString());
//        }
//    });
//});

app.ConfigureGlobalExceptionHandler(app);

app.UseHttpsRedirection();

app.UseCors(corsPolicy);

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
