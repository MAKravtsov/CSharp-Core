using FurnitureShop.Core.Clients.Domain.Clients.Data;
using FurnitureShop.Core.Clients.Domain.Clients.Requests;
using MediatR;

namespace FurnitureShop.Core.Clients.Domain.Clients.Handlers;

public class GetClientHandler
    : IRequestHandler<GetClientRequest, Client>
{
    public Task<Client> Handle(GetClientRequest request, CancellationToken cancellationToken)
    {
        return Task.FromResult(new Client
        {
            ClientId = request.ClientId,
            FirstName = "qwe",
            LastName = "123"
        });
    }
}