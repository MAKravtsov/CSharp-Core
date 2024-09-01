using System.Threading.Tasks;
using FurnitureShop.Core.Clients.Api.Contracts.Clients.Data;
using ProtoBuf.Grpc;
using ProtoBuf.Grpc.Configuration;

namespace FurnitureShop.Core.Clients.Api.Contracts.Clients;

[Service]
public interface IClientsService
{
    ValueTask<GetClientResponse> GetClientAsync(GetClientRequest request, CallContext context = default);
}