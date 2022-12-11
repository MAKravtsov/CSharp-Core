using AutoMapper;
using FurnitureShop.Core.Clients.Api.Contracts;
using Grpc.Core;
using MediatR;

namespace FurnitureShop.Core.Clients.Host.Services;

public class ClientsService : Api.Contracts.Clients.ClientsBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public ClientsService(
        IMediator mediator,
        IMapper mapper)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }
    
    public override async Task<GetClientResponse> GetClient(GetClientRequest request, ServerCallContext context)
    {
        var req = _mapper.Map<Domain.Clients.Requests.GetClientRequest>(request);
        var resp = await _mediator.Send(req);
        return _mapper.Map<GetClientResponse>(resp);
    }
}