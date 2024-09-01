using ProtoBuf;

namespace FurnitureShop.Core.Shop.Api.Contracts.FurnitureCatalog.Data;

[ProtoContract]
public class CatalogDto
{
    [ProtoMember(1)]
    public required string Title { get; set; }
}