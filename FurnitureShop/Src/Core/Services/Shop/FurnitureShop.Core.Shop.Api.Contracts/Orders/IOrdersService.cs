using System.Threading.Tasks;
using FurnitureShop.Core.Shop.Api.Contracts.Orders.Data;
using ProtoBuf.Grpc;
using ProtoBuf.Grpc.Configuration;

namespace FurnitureShop.Core.Shop.Api.Contracts.Orders;

[Service]
public interface IOrdersService
{
    ValueTask<GetClientOrdersResponse> GetClientOrdersAsync(GetClientOrdersRequest request, CallContext context = default);
}