using AutoMapper;
using FurnitureShop.Core.Clients.Api.Contracts.Clients;
using FurnitureShop.Core.Clients.Api.Contracts.Clients.Data;
using MediatR;
using ProtoBuf.Grpc;

namespace FurnitureShop.Core.Clients.Host.Services;

public class ClientsService : IClientsService
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
    
    public async ValueTask<GetClientResponse> GetClientAsync(GetClientRequest request, CallContext context = default)
    {
        var req = _mapper.Map<Domain.Clients.Requests.GetClientRequest>(request);
        var resp = await _mediator.Send(req);
        return _mapper.Map<GetClientResponse>(resp);
    }
}