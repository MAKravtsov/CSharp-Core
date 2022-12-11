using FurnitureShop.Core.Shop.Domain.Orders.Data;

namespace FurnitureShop.Core.Shop.Domain.Orders.Responses;

public class GetClientOrdersResponse
{
    public required Client Client { get; set; }
    
    public required IEnumerable<Order> Orders { get; set; }
}