using FurnitureShop.Core.Shop.Domain.Mappers;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace FurnitureShop.Core.Shop.Domain.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static void AddHandlers(this IServiceCollection serviceCollection)
    {
        AddGrpcClients(serviceCollection);
        serviceCollection.AddMediatR(typeof(ServiceCollectionExtensions).Assembly);
        AddMappers(serviceCollection);
    }

    private static void AddGrpcClients(IServiceCollection serviceCollection)
    {
        serviceCollection.AddGrpcClient<FurnitureShop.Core.Clients.Api.Contracts.Clients.ClientsClient>(options =>
        {
            options.Address = new Uri("http://localhost:1111");
        });
    }

    private static void AddMappers(IServiceCollection serviceCollection)
    {
        serviceCollection.AddAutoMapper(typeof(OrdersMapper));
    }
}