using AutoMapper;
using FurnitureShop.Core.Shop.Api.Contracts.FurnitureCatalog;
using FurnitureShop.Core.Shop.Api.Contracts.FurnitureCatalog.Data;
using MediatR;
using ProtoBuf.Grpc;

namespace FurnitureShop.Core.Shop.Host.Services;

public class FurnitureCatalogService : IFurnitureCatalogService
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

    public async ValueTask<GetFurnitureCatalogResponse> GetFurnitureCatalogAsync(CallContext context = default)
    {
        var resp = await _mediator.Send(new Domain.FurnitureCatalog.Requests.GetFurnitureCatalogRequest());
        return _mapper.Map<GetFurnitureCatalogResponse>(resp);
    }
}