using AutoMapper;
using FurnitureShop.Core.Clients.Api.Contracts.Clients;
using FurnitureShop.Core.Clients.Api.Contracts.Clients.Data;
using FurnitureShop.Core.Shop.Domain.Orders.Data;
using FurnitureShop.Core.Shop.Domain.Orders.Requests;
using FurnitureShop.Core.Shop.Domain.Orders.Responses;
using MediatR;

namespace FurnitureShop.Core.Shop.Domain.Orders.Handlers;

public class GetClientOrdersHandler : IRequestHandler<GetClientOrdersRequest, GetClientOrdersResponse>
{
    private readonly IMapper _mapper;
    private readonly IClientsService _clientsService;

    public GetClientOrdersHandler(
        IMapper mapper,
        IClientsService clientsService)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _clientsService = clientsService ?? throw new ArgumentNullException(nameof(clientsService));
    }
    
    public async Task<GetClientOrdersResponse> Handle(GetClientOrdersRequest request, CancellationToken cancellationToken)
    {
        var clientReq = _mapper.Map<GetClientRequest>(request);
        var client = await _clientsService.GetClientAsync(clientReq);

        if (client.Client == null)
        {
            throw new Exception($"Не найден клиент {clientReq.ClientId}");
        }
        
        var clientMap = _mapper.Map<Client>(client.Client);
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