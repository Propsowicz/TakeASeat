using Microsoft.EntityFrameworkCore;
using TakeASeat.Data.DatabaseContext;
using TakeASeat.IRepository;
using TakeASeat.Models;
using TakeASeat.Repository;

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












// BUILDING

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
