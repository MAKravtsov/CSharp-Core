using AutoMapper;
using FurnitureShop.Core.Shop.Api.Contracts;

namespace FurnitureShop.Core.Shop.Host.Mappers;

public class FurnitureCatalogMapper : Profile
{
    public FurnitureCatalogMapper()
    {
        CreateMap<ShowFurnitureCatalogRequest, Domain.FurnitureCatalog.Requests.ShowFurnitureCatalogRequest>();
        CreateMap<Domain.FurnitureCatalog.Data.Catalog, Catalog>();
    }
}