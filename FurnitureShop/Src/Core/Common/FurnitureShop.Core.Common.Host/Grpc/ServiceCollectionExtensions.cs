using Microsoft.Extensions.DependencyInjection;

namespace FurnitureShop.Core.Common.Host.Grpc;

public static class ServiceCollectionExtensions
{
    public static void AddFurnitureShopGrpc(
        this IServiceCollection services)
    {
        services.AddGrpc().AddJsonTranscoding();
    }
}