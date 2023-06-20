using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;

namespace RetailerStores.WebApi.Contexts
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<RetailerStoresDbContext>
    {
        public RetailerStoresDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            var builder = new DbContextOptionsBuilder<RetailerStoresDbContext>();

            var connectionString = configuration["DbConnectionString:ConnectionStringNpgsql"];
            builder
                .UseNpgsql(connectionString, o => o.MigrationsAssembly("RetailerStores.WebApi"));
            return new RetailerStoresDbContext(builder.Options);
        }
    }
}
