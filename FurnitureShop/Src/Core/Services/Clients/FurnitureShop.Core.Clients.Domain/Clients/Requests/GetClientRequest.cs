using FurnitureShop.Core.Clients.Domain.Clients.Data;
using MediatR;

namespace FurnitureShop.Core.Clients.Domain.Clients.Requests;

public class GetClientRequest : IRequest<Client>
{
    public required long ClientId { get; set; }
}