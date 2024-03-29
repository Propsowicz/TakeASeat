using Microsoft.EntityFrameworkCore;
using TakeASeat.BackgroundServices;
using TakeASeat.Data.DatabaseContext;
using System.Text.Json.Serialization;
using TakeASeat.ProgramConfigurations;
using TakeASeat.Services.UserService;
using TakeASeat.Services.EventService;
using TakeASeat.Services.ShowService;
using TakeASeat.Services.SeatService;
using TakeASeat.Services.SeatReservationService;
using TakeASeat.Services.BackgroundService;
using TakeASeat.Services.PaymentService;
using TakeASeat.Models.Configuration;
using TakeASeat.Services.EventTypesService;
using TakeASeat.Services.EventTagRepository;
using TakeASeat.Services.TicketService;


var builder = WebApplication.CreateBuilder(args);

// SERVICES
/// builder.Services.Configure*** are custom methods
builder.Services.AddControllers();

// Get Keys from appsettings.json
builder.Services.AddOptions();
builder.Services.Configure<EmailProviderData>(
                builder.Configuration.GetSection("EmailProviderData"));
builder.Services.Configure<PaymentServerData>(
                builder.Configuration.GetSection("PaymentServerData"));
builder.Services.Configure<ApiAuthKey>(
                builder.Configuration.GetSection("ApiAuthKey"));
// CORS
var corsPolicy = "_corsPolicy";
builder.Services.ConfigureCORS(corsPolicy);

// LOGS
builder.Host.ConfigureSerilog();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.ConfigureSwaggerGen();

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
builder.Services.AddScoped<IAuthManager, AuthManager>();
builder.Services.AddScoped<IEventRepository, EventRepository>();
builder.Services.AddScoped<IShowRepository, ShowRepository>();
builder.Services.AddScoped<ISeatRepository, SeatRepository>();
builder.Services.AddScoped<ISeatResRepository, SeatResRepository>();
builder.Services.AddScoped<IReleaseReservationService, ReleaseReservationService>();
builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
builder.Services.AddScoped<IEventTypeRepository, EventTypeRepository>();
builder.Services.AddScoped<IEventTagRepository, EventTagRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ITicketRepository, TicketRepository>();

// Backgorund service
builder.Services.AddHostedService<ReleaseReservation>();

//JWT
builder.Services.AddAuthentication();
builder.Services.ConfigureIdentity();
builder.Services.ConfigureJWT(builder.Configuration);

// API versioning
builder.Services.ConfigureApiVersioning();

// BUILDING
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    using (var scope = app.Services.CreateScope())
    {
        var DatabaseContext = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
        DatabaseContext.Database.EnsureCreated();
    }
}

app.ConfigureGlobalExceptionHandler(app);

app.UseHttpsRedirection();

app.UseCors(corsPolicy);

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
public partial class Program { } // so you can reference it from tests

