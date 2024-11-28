using FurnitureShop.Core.Shop.Api.Contracts.FurnitureCatalog.Data;
using ProtoBuf.Grpc;
using ProtoBuf.Grpc.Configuration;

namespace FurnitureShop.Core.Shop.Api.Contracts.FurnitureCatalog;

[Service]
public interface IFurnitureCatalogService
{
    ValueTask<GetFurnitureCatalogResponse> GetFurnitureCatalogAsync(CallContext context = default);
}