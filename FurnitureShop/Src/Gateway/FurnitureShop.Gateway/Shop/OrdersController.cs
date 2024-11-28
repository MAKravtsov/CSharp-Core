using FurnitureShop.Core.Shop.Api.Contracts.Orders;
using FurnitureShop.Core.Shop.Api.Contracts.Orders.Data;
using Microsoft.AspNetCore.Mvc;

namespace FurnitureShop.Gateway.Shop;

[ApiController]
[Route("orders")]
public class OrdersController : ControllerBase
{
    private readonly IOrdersService _ordersService;

    public OrdersController(IOrdersService ordersService)
    {
        _ordersService = ordersService;
    }
    
    [HttpGet]
    public ValueTask<GetClientOrdersResponse> GetClientOrders([FromQuery] GetClientOrdersRequest request)
    {
        return _ordersService.GetClientOrdersAsync(request);
    }
}