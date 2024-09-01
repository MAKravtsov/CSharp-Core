using Microsoft.Extensions.DependencyInjection;

namespace FurnitureShop.Core.Common.Host.Grpc;

public static class ServiceCollectionExtensions
{
    [Obsolete("с protobuf-net не работает. Пока не реализовано")]
    public static void AddFurnitureShopGrpc(
        this IServiceCollection services)
    {
        services.AddGrpc().AddJsonTranscoding();
    }
}