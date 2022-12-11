namespace FurnitureShop.Core.Clients.Domain.Clients.Data;

public class Client
{
    public required long ClientId { get; set; }
    
    public required string FirstName { get; set; }
    
    public required string LastName { get; set; }
}