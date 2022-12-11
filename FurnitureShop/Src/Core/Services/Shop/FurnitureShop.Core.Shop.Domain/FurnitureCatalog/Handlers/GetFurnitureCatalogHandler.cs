using FurnitureShop.Core.Shop.Domain.FurnitureCatalog.Data;
using FurnitureShop.Core.Shop.Domain.FurnitureCatalog.Requests;
using FurnitureShop.Core.Shop.Domain.FurnitureCatalog.Response;
using MediatR;

namespace FurnitureShop.Core.Shop.Domain.FurnitureCatalog.Handlers;

public class GetFurnitureCatalogHandler 
    : IRequestHandler<GetFurnitureCatalogRequest, GetFurnitureCatalogResponse>
{
    public Task<GetFurnitureCatalogResponse> Handle(
        GetFurnitureCatalogRequest request, 
        CancellationToken cancellationToken)
    {
        return Task.FromResult(new GetFurnitureCatalogResponse
        {
            Catalog = new Catalog
            {
                Title = "Каталог мебели",
            },
        });
    }
}