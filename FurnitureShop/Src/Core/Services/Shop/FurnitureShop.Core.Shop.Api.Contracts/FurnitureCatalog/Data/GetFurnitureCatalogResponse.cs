using ProtoBuf;

namespace FurnitureShop.Core.Shop.Api.Contracts.FurnitureCatalog.Data;

[ProtoContract]
public class GetFurnitureCatalogResponse
{
    public required CatalogDto Catalog { get; set; }
}