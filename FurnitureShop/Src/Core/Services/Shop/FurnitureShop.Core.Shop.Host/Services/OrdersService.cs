using AutoMapper;
using FurnitureShop.Core.Shop.Api.Contracts.Orders;
using FurnitureShop.Core.Shop.Api.Contracts.Orders.Data;
using MediatR;
using ProtoBuf.Grpc;

namespace FurnitureShop.Core.Shop.Host.Services;

public class OrdersService : IOrdersService
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public OrdersService(
        IMediator mediator,
        IMapper mapper)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }
    
    public async ValueTask<GetClientOrdersResponse> GetClientOrdersAsync(GetClientOrdersRequest request, CallContext context = default)
    {
        var req = _mapper.Map<Domain.Orders.Requests.GetClientOrdersRequest>(request);
        var resp = await _mediator.Send(req);
        return _mapper.Map<GetClientOrdersResponse>(resp);
    }
}