using FurnitureShop.Core.Shop.Domain.FurnitureCatalog.Data;

namespace FurnitureShop.Core.Shop.Domain.FurnitureCatalog.Response;

public class GetFurnitureCatalogResponse
{
    public required Catalog Catalog { get; set; }
}