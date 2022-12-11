using AutoMapper;
using FurnitureShop.Core.Clients.Api.Contracts;
using FurnitureShop.Core.Shop.Domain.Orders.Data;
using FurnitureShop.Core.Shop.Domain.Orders.Requests;
using FurnitureShop.Core.Shop.Domain.Orders.Responses;
using MediatR;

namespace FurnitureShop.Core.Shop.Domain.Orders.Handlers;

public class GetClientOrdersHandler : IRequestHandler<GetClientOrdersRequest, GetClientOrdersResponse>
{
    private readonly IMapper _mapper;
    private readonly Core.Clients.Api.Contracts.Clients.ClientsClient _clientsClient;

    public GetClientOrdersHandler(
        IMapper mapper,
        Core.Clients.Api.Contracts.Clients.ClientsClient clientsClient)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _clientsClient = clientsClient ?? throw new ArgumentNullException(nameof(clientsClient));
    }
    
    public async Task<GetClientOrdersResponse> Handle(GetClientOrdersRequest request, CancellationToken cancellationToken)
    {
        var clientReq = _mapper.Map<GetClientRequest>(request);
        var client = await _clientsClient.GetClientAsync(clientReq);

        if (client?.Client == null)
        {
            throw new Exception($"Не найден клиент {clientReq.ClientId}");
        }
        
        var clientMap = _mapper.Map<Data.Client>(client.Client);
        var orders = new[]
        {
            new Order
            {
                Id = Guid.NewGuid(),
                Name = "шкаф"
            },
            new Order
            {
                Id = Guid.NewGuid(),
                Name = "диван"
            }
        };
        
        return new GetClientOrdersResponse
        {
            Client = clientMap,
            Orders = orders,
        };
    }
}