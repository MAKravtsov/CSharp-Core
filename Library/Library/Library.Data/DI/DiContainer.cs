using Library.Common.DependancyInjection;
using Library.Common.Extensions;
using Library.Data.Infrastructure.Contexts;
using Library.Data.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Library.Data.DI
{
    public class DiContainer
    {
        private readonly IServiceCollection _services;

        private readonly IDependanciesRegistrator _dependanciesRegistrator;

        public DiContainer(IServiceCollection services, IDependanciesRegistrator dependanciesRegistrator)
        {
            _services = services;
            _dependanciesRegistrator = dependanciesRegistrator;
        }

        public void Build()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("DatabaseSettings.json")
                .Build();

            var connectionString = config.GetConnectionString("LibraryConnectionString");

            _services.AddDbContext<LibraryContext>(options => options.UseNpgsql(connectionString));

            _dependanciesRegistrator.RegisterFromBaseType(typeof(BaseRepository<LibraryContext>), _services.GetAddScopedDelegate());
        }
    }
}
