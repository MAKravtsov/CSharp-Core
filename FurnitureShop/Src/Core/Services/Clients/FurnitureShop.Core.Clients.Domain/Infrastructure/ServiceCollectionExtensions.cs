using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace FurnitureShop.Core.Clients.Domain.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static void AddHandlers(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddMediatR(typeof(ServiceCollectionExtensions).Assembly);
    }
}