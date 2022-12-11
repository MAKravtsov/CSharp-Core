using FurnitureShop.Core.Clients.Host.Mappers;

namespace FurnitureShop.Core.Clients.Host.Infrastructure;

public static class ServiceCollectionExtensions
{
    internal static void AddMappers(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddAutoMapper(typeof(ClientsMapper));
    }
}