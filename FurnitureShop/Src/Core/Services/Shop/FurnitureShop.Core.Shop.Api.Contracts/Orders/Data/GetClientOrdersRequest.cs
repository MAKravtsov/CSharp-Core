using ProtoBuf;

namespace FurnitureShop.Core.Shop.Api.Contracts.Orders.Data;

[ProtoContract]
public class GetClientOrdersRequest
{
    [ProtoMember(1)]
    public required long ClientId { get; set; }
}