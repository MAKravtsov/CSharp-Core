namespace FurnitureShop.Core.Shop.Domain.Orders.Data;

public class Client
{
    public required long ClientId { get; set; }
    
    public required string FirstName { get; set; }
    
    public required string LastName { get; set; }
}