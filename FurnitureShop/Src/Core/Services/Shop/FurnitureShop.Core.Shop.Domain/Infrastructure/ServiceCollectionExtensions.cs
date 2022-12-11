using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace FurnitureShop.Core.Shop.Domain.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static void AddHandlers(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddGrpcClient<FurnitureShop.Core.Clients.Api.Contracts.Clients.ClientsClient>(options =>
        {
            options.Address = new Uri("http://localhost:1111");
        });

        serviceCollection.AddMediatR(typeof(ServiceCollectionExtensions).Assembly);
    }
}