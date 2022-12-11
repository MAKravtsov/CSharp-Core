using FurnitureShop.Core.Shop.Domain.Orders.Responses;
using MediatR;

namespace FurnitureShop.Core.Shop.Domain.Orders.Requests;

public class GetClientOrdersRequest : IRequest<GetClientOrdersResponse>
{
    public required long ClientId { get; set; }
}