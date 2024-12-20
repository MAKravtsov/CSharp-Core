using AutoMapper;
using FurnitureShop.Core.Shop.Api.Contracts.FurnitureCatalog.Data;

namespace FurnitureShop.Core.Shop.Host.Mappers;

public class FurnitureCatalogMapper : Profile
{
    public FurnitureCatalogMapper()
    {
        CreateMap<Domain.FurnitureCatalog.Response.GetFurnitureCatalogResponse, GetFurnitureCatalogResponse>();
        CreateMap<Domain.FurnitureCatalog.Data.Catalog, CatalogDto>();
    }
}