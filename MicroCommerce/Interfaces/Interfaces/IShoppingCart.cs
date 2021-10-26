using Interfaces.Models;
using Shed.CoreKit.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces.Interfaces
{
    public interface IShoppingCart
    {
        Cart Get();

        [HttpPut, Route("addorder/{productId}/{qty}")]
        Cart AddOrder(Guid productId, int qty);

        [HttpGet, Route("test")]
        string  Test();
        
        Cart DeleteOrder(Guid orderId);

        [Route("getevents/{timestamp}")]
        IEnumerable<CartEvent> GetCartEvents(long timestamp);
    }
}
