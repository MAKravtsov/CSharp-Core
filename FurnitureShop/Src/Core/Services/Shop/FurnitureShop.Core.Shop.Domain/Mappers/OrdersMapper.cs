using AutoMapper;
using FurnitureShop.Core.Clients.Api.Contracts.Clients.Data;
using FurnitureShop.Core.Shop.Domain.Orders.Requests;

namespace FurnitureShop.Core.Shop.Domain.Mappers;

public class OrdersMapper : Profile
{
    public OrdersMapper()
    {
        CreateMap<GetClientOrdersRequest, GetClientRequest>();
        CreateMap<ClientDto, Orders.Data.Client>();
    }
}