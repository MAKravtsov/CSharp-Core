using FurnitureShop.Core.Shop.Host.Mappers;

namespace FurnitureShop.Core.Shop.Host.Infrastructure;

public static class ServiceCollectionExtensions
{
    internal static void AddMappers(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddAutoMapper(typeof(FurnitureCatalogMapper));
    }
}