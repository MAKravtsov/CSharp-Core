namespace FurnitureShop.Core.Shop.Domain.Orders.Data;

public class Order
{
    public required Guid Id { get; set; }
    
    public required string Name { get; set; }
}