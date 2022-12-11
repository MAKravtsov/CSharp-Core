using AutoMapper;
using FurnitureShop.Core.Shop.Api.Contracts;

namespace FurnitureShop.Core.Shop.Host.Mappers;

public class OrdersMapper : Profile
{
    public OrdersMapper()
    {
        CreateMap<GetClientOrdersRequest, Domain.Orders.Requests.GetClientOrdersRequest>();
        CreateMap<Domain.Orders.Responses.GetClientOrdersResponse, GetClientOrdersResponse>();
        CreateMap<Domain.Orders.Data.Order, Order>()
            .ForMember(dest => dest.Id, 
                opt => opt.MapFrom(y => y.Id.ToString()));
        CreateMap<Domain.Orders.Data.Client, Client>();
    }
}