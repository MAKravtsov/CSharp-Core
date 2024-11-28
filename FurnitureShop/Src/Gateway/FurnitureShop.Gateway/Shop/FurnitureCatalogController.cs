using FurnitureShop.Core.Shop.Api.Contracts.FurnitureCatalog;
using FurnitureShop.Core.Shop.Api.Contracts.FurnitureCatalog.Data;
using Microsoft.AspNetCore.Mvc;

namespace FurnitureShop.Gateway.Shop;

[ApiController]
[Route("furniture-catalog")]
public class FurnitureCatalogController : ControllerBase
{
    private readonly IFurnitureCatalogService _furnitureCatalogService;

    public FurnitureCatalogController(IFurnitureCatalogService furnitureCatalogService)
    {
        _furnitureCatalogService = furnitureCatalogService;
    }

    [HttpGet]
    public ValueTask<GetFurnitureCatalogResponse> GetFurnitureCatalogAsync()
    {
        return _furnitureCatalogService.GetFurnitureCatalogAsync();
    }
}