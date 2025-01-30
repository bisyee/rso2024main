using Microsoft.EntityFrameworkCore;
using ParkingService.Data;
using ParkingService.Services;
using ParkingService.Repositories;
using Swashbuckle.AspNetCore.SwaggerGen;

var builder = WebApplication.CreateBuilder(args);

builder.Host.ConfigureAppConfiguration((context, config) =>
{
    config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
    config.AddEnvironmentVariables();
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://20.253.19.10", "localhost", "http://localhost:5094")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});
builder.Services.AddDbContext<UserContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddScoped<IUserRepository, UserRepositoryy>();
builder.Services.AddScoped<IUserService, UserService>();


builder.Services.AddHealthChecks();
builder.Services.AddHttpClient();

var app = builder.Build();
app.MapHealthChecks("/health");

app.UseSwagger();
app.UseSwaggerUI();


app.UseStaticFiles();

app.UseCors("AllowFrontend");
app.UseRouting();
app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();
