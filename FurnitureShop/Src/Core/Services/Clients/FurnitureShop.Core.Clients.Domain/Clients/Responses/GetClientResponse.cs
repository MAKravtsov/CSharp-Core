using FurnitureShop.Core.Clients.Domain.Clients.Data;

namespace FurnitureShop.Core.Clients.Domain.Clients.Responses;

public class GetClientResponse
{
    public required Client Client { get; set; }
}