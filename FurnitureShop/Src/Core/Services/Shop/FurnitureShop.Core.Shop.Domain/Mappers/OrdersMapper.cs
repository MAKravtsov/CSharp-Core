using AutoMapper;
using FurnitureShop.Core.Clients.Api.Contracts;
using FurnitureShop.Core.Shop.Domain.Orders.Requests;

namespace FurnitureShop.Core.Shop.Domain.Mappers;

public class OrdersMapper : Profile
{
    public OrdersMapper()
    {
        CreateMap<GetClientOrdersRequest, GetClientRequest>();
        CreateMap<Client, Orders.Data.Client>();
    }
}