using FurnitureShop.Core.Shop.Api.Contracts.FurnitureCatalog;
using FurnitureShop.Core.Shop.Api.Contracts.Orders;
using ProtoBuf.Grpc.ClientFactory;

namespace FurnitureShop.Gateway.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddClients(this IServiceCollection services)
    {
        var uri = new Uri("http://localhost:1112");

        services.AddGrpcClient<IOrdersService>(uri);
        services.AddGrpcClient<IFurnitureCatalogService>(uri);

        return services;
    }

    private static void AddGrpcClient<T>(this IServiceCollection services, Uri uri) where T : class
    {
        services.AddCodeFirstGrpcClient<T>(options => options.Address = uri);
    }
}