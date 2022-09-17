using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Library.Data.Infrastructure.Contexts
{
    public class LibraryContextFactory : IDesignTimeDbContextFactory<LibraryContext>
    {
        public LibraryContext CreateDbContext(string[] args)
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("DatabaseSettings.json")
                .Build();

            var connectionString = config.GetConnectionString("LibraryConnectionString");

            var optionsBuilder = new DbContextOptionsBuilder<LibraryContext>();
            optionsBuilder.UseNpgsql(connectionString);

            return new LibraryContext(optionsBuilder.Options);
        }
    }
}
