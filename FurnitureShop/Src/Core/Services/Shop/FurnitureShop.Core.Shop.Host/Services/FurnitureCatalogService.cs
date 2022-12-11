using AutoMapper;
using FurnitureShop.Core.Shop.Api.Contracts;
using Grpc.Core;
using MediatR;

namespace FurnitureShop.Core.Shop.Host.Services;

public class FurnitureCatalogService : FurnitureCatalog.FurnitureCatalogBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public FurnitureCatalogService(
        IMediator mediator,
        IMapper mapper)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }
    
    public override async Task<Catalog> ShowFurnitureCatalog(ShowFurnitureCatalogRequest request, ServerCallContext context)
    {
        var req = _mapper.Map<Domain.FurnitureCatalog.Requests.ShowFurnitureCatalogRequest>(request);
        var resp = await _mediator.Send(req);
        return _mapper.Map<Catalog>(resp);
    }
}