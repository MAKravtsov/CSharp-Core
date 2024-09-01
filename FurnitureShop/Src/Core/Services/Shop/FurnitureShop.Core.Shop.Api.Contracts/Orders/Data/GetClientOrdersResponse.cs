using ProtoBuf;

namespace FurnitureShop.Core.Shop.Api.Contracts.Orders.Data;

[ProtoContract]
public class GetClientOrdersResponse
{
    [ProtoMember(1)]
    public required ClientDto Client { get; set; }
    
    [ProtoMember(2)]
    public required OrderDto[] Orders { get; set; }
}