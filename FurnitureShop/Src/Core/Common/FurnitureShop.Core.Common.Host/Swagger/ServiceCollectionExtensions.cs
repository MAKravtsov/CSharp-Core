using Microsoft.Extensions.DependencyInjection;

namespace FurnitureShop.Core.Common.Host.Swagger;

public static class ServiceCollectionExtensions
{
    public static void AddFurnitureShopSwagger(
        this IServiceCollection services)
    {
        services.AddGrpcSwagger();
        services.AddSwaggerGen();
    }
}