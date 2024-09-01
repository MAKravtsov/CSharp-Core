using ProtoBuf;

namespace FurnitureShop.Core.Clients.Api.Contracts.Clients.Data;

[ProtoContract]
public class GetClientRequest
{
    [ProtoMember(1)]
    public required long ClientId { get; set; }
}