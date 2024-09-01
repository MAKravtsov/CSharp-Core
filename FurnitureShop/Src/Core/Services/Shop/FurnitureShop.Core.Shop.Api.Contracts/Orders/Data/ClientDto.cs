using ProtoBuf;

namespace FurnitureShop.Core.Shop.Api.Contracts.Orders.Data;

[ProtoContract]
public class ClientDto
{
    [ProtoMember(1)]
    public required long ClientId { get; set; }
    
    [ProtoMember(2)]
    public required string FirstName { get; set; }
    
    [ProtoMember(3)]
    public required string LastName { get; set; }
}