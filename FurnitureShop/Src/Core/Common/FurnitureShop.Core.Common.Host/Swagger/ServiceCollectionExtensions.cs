using Microsoft.Extensions.DependencyInjection;

namespace FurnitureShop.Core.Common.Host.Swagger;

public static class ServiceCollectionExtensions
{
    [Obsolete("с protobuf-net не работает. Пока не реализовано")]
    public static void AddFurnitureShopSwagger(
        this IServiceCollection services)
    {
        services.AddGrpcSwagger();
        services.AddSwaggerGen();
    }
}