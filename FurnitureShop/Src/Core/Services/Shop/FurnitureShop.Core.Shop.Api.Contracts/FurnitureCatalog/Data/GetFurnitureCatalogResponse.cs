using ProtoBuf;

namespace FurnitureShop.Core.Shop.Api.Contracts.FurnitureCatalog.Data;

[ProtoContract]
public class GetFurnitureCatalogResponse
{
    [ProtoMember(1)]
    public required CatalogDto Catalog { get; set; }
}