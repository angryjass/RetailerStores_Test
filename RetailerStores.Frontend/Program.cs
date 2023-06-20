using Microsoft.Extensions.Configuration;
using RetailerStores.Frontend.Interfaces;
using RetailerStores.Frontend.Services;

var builder = WebApplication.CreateBuilder(args);

var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services
    .AddScoped<IStoreService, StoreService>()
    .AddScoped<IStockService, StockService>()
    .AddScoped<IMetricService, MetricService>();

builder.Services.AddHttpClient<IStoreService, StoreService>(client =>
{
        client.BaseAddress = new Uri(configuration["backendUrl"]);
    });

builder.Services.AddHttpClient<IStockService, StockService>(client =>
    {
        client.BaseAddress = new Uri(configuration["backendUrl"]);
    });

builder.Services.AddHttpClient<IMetricService, MetricService>(client =>
    {
        client.BaseAddress = new Uri(configuration["backendUrl"]);
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
