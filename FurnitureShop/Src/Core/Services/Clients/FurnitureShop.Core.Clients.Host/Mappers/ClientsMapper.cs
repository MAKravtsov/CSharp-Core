using AutoMapper;
using FurnitureShop.Core.Clients.Api.Contracts.Clients.Data;

namespace FurnitureShop.Core.Clients.Host.Mappers;

public class ClientsMapper : Profile
{
    public ClientsMapper()
    {
        CreateMap<GetClientRequest, Domain.Clients.Requests.GetClientRequest>();
        CreateMap<Domain.Clients.Responses.GetClientResponse, GetClientResponse>();
        CreateMap<Domain.Clients.Data.Client, ClientDto>();
    }
}