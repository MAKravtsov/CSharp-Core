using FurnitureShop.Core.Shop.Domain.Mappers;
using Microsoft.Extensions.DependencyInjection;
using ProtoBuf.Grpc.ClientFactory;

namespace FurnitureShop.Core.Shop.Domain.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static void AddHandlers(this IServiceCollection serviceCollection)
    {
        AddGrpcClients(serviceCollection);
        serviceCollection.AddMediatR(conf => conf
            .RegisterServicesFromAssembly(typeof(ServiceCollectionExtensions).Assembly));
        AddMappers(serviceCollection);
    }

    private static void AddGrpcClients(IServiceCollection serviceCollection)
    {
        serviceCollection.AddCodeFirstGrpcClient<FurnitureShop.Core.Clients.Api.Contracts.Clients.IClientsService>(options =>
        {
            options.Address = new Uri("http://localhost:1111");
        });
    }

    private static void AddMappers(IServiceCollection serviceCollection)
    {
        serviceCollection.AddAutoMapper(typeof(OrdersMapper));
    }
}