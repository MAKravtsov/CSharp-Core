using System;
using ProtoBuf;

namespace FurnitureShop.Core.Shop.Api.Contracts.Orders.Data;

[ProtoContract]
public class OrderDto
{
    [ProtoMember(1)]
    public required Guid Id { get; set; }
    
    [ProtoMember(2)]
    public required string Name { get; set; }
}