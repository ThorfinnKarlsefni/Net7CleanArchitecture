using Logistics.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
namespace Logistics.Infrastructure;

public class DbContextFactory : IDesignTimeDbContextFactory<XhwtDbContext>
{
    public XhwtDbContext CreateDbContext(string[] args)
    {
         var optionsBuilder = new DbContextOptionsBuilder<XhwtDbContext>();
            optionsBuilder.UseNpgsql("Host=;Database=;Username=postgres;Password=;");

            return new XhwtDbContext(optionsBuilder.Options);
    }
}

