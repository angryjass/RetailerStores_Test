using AutoMapper.Extensions.ExpressionMapping;
using FluentValidation;
using Hellang.Middleware.ProblemDetails;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Internal;
using Microsoft.OpenApi.Models;
using RetailerStores.Application.Interfaces;
using RetailerStores.Application.Stores;
using RetailerStores.Application.Stores.Queries;
using RetailerStores.WebApi.Contexts;
using RetailerStores.WebApi.ExcelParser;
using RetailerStores.WebApi.Extensions;
using System.Reflection;

var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwaggerGen(options =>
{
    var swaggerFileName = nameof(RetailerStores.WebApi) + ".xml";
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = swaggerFileName,
    });
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, swaggerFileName));
});

builder.Services.AddControllers();

builder.Services.AddSingleton<ISystemClock>(new SystemClock());

builder.Services.AddDbContext<IRetailerStoresDbContext, RetailerStoresDbContext>(c =>
{
    var connectionString = configuration["DbConnectionString:ConnectionStringNpgsql"];
    c.UseNpgsql(connectionString);

#if RELEASE
    using (var context = new RetailerStoresDbContext((c as DbContextOptionsBuilder<RetailerStoresDbContext>)!.Options))
    {
        context.Database.Migrate();
    }
#endif
});

builder.Services.AddAutoMapper(
                    cfg => cfg.AddExpressionMapping(),
                    typeof(StoreMappingProfile).GetTypeInfo().Assembly);

builder.Services.RegisterValidators();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies())); ;

builder.Services.ConfigureProblemDetails();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "API ver. v1");
});

app.UseAuthorization();

app.MapControllers();

app.UseProblemDetails();

app.Run();
