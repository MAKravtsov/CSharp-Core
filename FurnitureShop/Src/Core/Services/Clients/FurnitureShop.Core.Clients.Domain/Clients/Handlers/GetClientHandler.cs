using FurnitureShop.Core.Clients.Domain.Clients.Data;
using FurnitureShop.Core.Clients.Domain.Clients.Requests;
using FurnitureShop.Core.Clients.Domain.Clients.Responses;
using MediatR;

namespace FurnitureShop.Core.Clients.Domain.Clients.Handlers;

public class GetClientHandler
    : IRequestHandler<GetClientRequest, GetClientResponse>
{
    public Task<GetClientResponse> Handle(GetClientRequest request, CancellationToken cancellationToken)
    {
        return Task.FromResult(new GetClientResponse
        {
            Client = new Client
            {
                ClientId = request.ClientId,
                FirstName = "qwe",
                LastName = "123"
            }
        });
    }
}