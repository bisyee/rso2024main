using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using frontend;
using ParkingService.Client;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);


builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5000/") });

builder.Services.AddScoped<ParkingApiService>();
builder.Services.AddScoped<ReservationApiService>();
builder.Services.AddScoped<UserApiService>();

await builder.Build().RunAsync();
