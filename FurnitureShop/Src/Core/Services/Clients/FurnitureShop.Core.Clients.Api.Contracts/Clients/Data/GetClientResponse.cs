using ProtoBuf;

namespace FurnitureShop.Core.Clients.Api.Contracts.Clients.Data;

[ProtoContract]
public class GetClientResponse
{
    [ProtoMember(1)]
    public required ClientDto Client { get; set; }
}