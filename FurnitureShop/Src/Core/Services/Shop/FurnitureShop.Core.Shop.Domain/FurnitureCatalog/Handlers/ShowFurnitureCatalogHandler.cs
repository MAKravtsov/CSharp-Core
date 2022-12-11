using FurnitureShop.Core.Clients.Api.Contracts;
using FurnitureShop.Core.Shop.Domain.FurnitureCatalog.Data;
using FurnitureShop.Core.Shop.Domain.FurnitureCatalog.Requests;
using MediatR;

namespace FurnitureShop.Core.Shop.Domain.FurnitureCatalog.Handlers;

public class ShowFurnitureCatalogHandler 
    : IRequestHandler<ShowFurnitureCatalogRequest, Catalog>
{
    private readonly Core.Clients.Api.Contracts.Clients.ClientsClient _clientsClient;

    public ShowFurnitureCatalogHandler(
        Core.Clients.Api.Contracts.Clients.ClientsClient clientsClient)
    {
        _clientsClient = clientsClient ?? throw new ArgumentNullException(nameof(clientsClient));
    }
    
    public async Task<Catalog> Handle(
        ShowFurnitureCatalogRequest request, 
        CancellationToken cancellationToken)
    {
        var getClientRequest = new GetClientRequest
        {
            ClientId = 2
        };
        var client = await _clientsClient.GetClientAsync(getClientRequest);
        return new Catalog
        {
            Title = client.FirstName
        };
    }
}