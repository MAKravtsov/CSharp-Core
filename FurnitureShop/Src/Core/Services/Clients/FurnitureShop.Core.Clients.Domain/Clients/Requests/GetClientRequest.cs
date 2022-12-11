using FurnitureShop.Core.Clients.Domain.Clients.Responses;
using MediatR;

namespace FurnitureShop.Core.Clients.Domain.Clients.Requests;

public class GetClientRequest : IRequest<GetClientResponse>
{
    public required long ClientId { get; set; }
}