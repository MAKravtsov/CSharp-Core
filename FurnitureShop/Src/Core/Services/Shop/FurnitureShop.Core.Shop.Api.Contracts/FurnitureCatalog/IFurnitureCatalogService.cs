using System.Threading.Tasks;
using FurnitureShop.Core.Shop.Api.Contracts.FurnitureCatalog.Data;
using Grpc.Core;
using ProtoBuf.Grpc.Configuration;

namespace FurnitureShop.Core.Shop.Api.Contracts.FurnitureCatalog;

[Service]
public interface IFurnitureCatalogService
{
    ValueTask<GetFurnitureCatalogResponse> GetFurnitureCatalogAsync(GetFurnitureCatalogRequest request, ServerCallContext context = default);
}