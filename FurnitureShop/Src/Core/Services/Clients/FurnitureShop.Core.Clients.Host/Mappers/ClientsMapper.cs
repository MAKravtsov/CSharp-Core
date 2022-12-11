using AutoMapper;
using FurnitureShop.Core.Clients.Api.Contracts;

namespace FurnitureShop.Core.Clients.Host.Mappers;

public class ClientsMapper : Profile
{
    public ClientsMapper()
    {
        CreateMap<GetClientRequest, Domain.Clients.Requests.GetClientRequest>();
        CreateMap<Domain.Clients.Data.Client, Client>();
    }
}